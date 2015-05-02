using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TractorBeamScript : MonoBehaviour {

	UnityStandardAssets.Characters.FirstPerson.FirstPersonController controller;

	// Use this for initialization
	void Start () {
		controller = gameObjectToFloat.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
	}

	public float disFromParticle = .5f;
	public bool tractorBeamOn = false;

	public float radius = 1f;
	public float frequency = 20f;
	public float updateSpeed = 10f;

	private float offset = 0f;

	public Vector3 origin;
	public Vector3 end;

	//Stuff related to pulling the character along the tractor beam
	public GameObject gameObjectToFloat;
	public float tractorRadius = 2f;

	public float expireTime = 3f;
	float expireCount = 0f;

	public float expireRadius = 3f;

	void DisableTractorBeam()
	{

		int particlesCount = ParticleScript.allParticles.Count;
		for(int i = 0; i < particlesCount; i++)
		{
			ParticleScript particle = ParticleScript.allParticles[i];
			particle.headToPosition = false;
		}
		tractorBeamOn = false;
		expireCount = 0;
	}

    float velocityMagnitude = 0f;

	Vector3 velocity;
    Vector3 dir;

	public float acceleration = .3f;
	public float friction = .2f;
	public float maxSpeed = 15;

	List<Vector3> points = new List<Vector3>();

	public float pointFrequency = .1f;
	float pointCount = 0;
	float disTraveled = 0f;

	Vector3 previousPos;

    public bool unlimitedFlight = true;

    bool justTractored = true;
	// Update is called once per frame
	void Update () {
	
		bool grounded = gameObjectToFloat.GetComponent<CharacterController>().isGrounded;
		if(!grounded)
		{
            gameObjectToFloat.GetComponent<CharacterController>().Move(dir * velocityMagnitude * Time.deltaTime);//velocity * Time.deltaTime);
		}
		else
		{
			disTraveled = 0;
		}
		//friction

        velocityMagnitude -= friction * Time.deltaTime;
		//velocity -= velocity * friction * Time.deltaTime;
		
		//controller.AddToMoveDir(-1f controller.ge* friction * Time.deltaTime;

		if((Input.GetMouseButton(0) || Input.GetAxis("TractorBeam") > .2f) && (disTraveled < ParticleScript.allParticles.Count * disFromParticle || unlimitedFlight))
		{
			if(justTractored)
			{
				points.Clear();
				previousPos = transform.position;
			}
            justTractored = true;
			tractorBeamOn = true;
			disTraveled += (previousPos - transform.position).magnitude;
			previousPos = transform.position;

			//Target to be accelerated towards
			Vector3 target = transform.forward;

			pointCount += Time.deltaTime;
			if(pointCount > pointFrequency)
			{
				points.Add(transform.position + target*4f);
			}

            dir = target;

            float realMaxSpeed = maxSpeed;

            float angleDown = Vector3.Angle(Vector3.down, dir);

            float speedModifier = 1f;

            if(angleDown < 50f)
            {
                speedModifier = (1f - (angleDown / 50f)) * 1.5f;
                realMaxSpeed += (realMaxSpeed ) * speedModifier;
            }

            if (velocityMagnitude < realMaxSpeed) //velocity.magnitude < maxSpeed || 
			{
                velocityMagnitude += acceleration * speedModifier * Time.deltaTime;
				//velocity += target * acceleration * Time.deltaTime;
			}
			//controller.AddToMoveDir(target * acceleration * Time.deltaTime);

			//velocity += controller.GetMoveDir();
			controller.DisableGravity = true;
			//.AddForce((origin - end).normalized * 30f);

			/*
			if(tractorBeamOn && false)
			{
				DisableTractorBeam();
			}
			else
			{
				tractorBeamOn = true;

				int particleCount = ParticleScript.allParticles.Count;

				float distance = particleCount * disFromParticle;
				RaycastHit hit;

				origin = transform.position;

				if(Physics.Raycast(new Ray(this.transform.position, this.transform.forward), out hit, distance))
				{
					end = hit.point;
				}
				else
				{
					end = this.transform.position + this.transform.forward * distance;
				}
				expireCount = 0;

			}*/
			expireCount = 0;

		}
		else
		{
            justTractored = false;
			expireCount += Time.deltaTime;
			if(expireCount > expireTime)
			{
				DisableTractorBeam();
				expireCount = 0;
			}
			gameObjectToFloat.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().DisableGravity = false;
		}

		if(tractorBeamOn && ParticleScript.allParticles.Count > 0)
		{

				expireCount = 0;
			
				offset -= updateSpeed * Time.deltaTime;

				frequency = .5f + Mathf.Sin (offset/20f)* 100f;
				radius = 1.5f + Mathf.Sin (offset/20f)* .5f;

				int particlesCount = ParticleScript.allParticles.Count;

				

				float realDisPerParticle = (end - origin).magnitude / (float)particlesCount;

				int total = 0;
				float leftOver = 0f;
				for(int j = 0; j < points.Count-1; j++)
				{
					Vector3 dif = points[j+1] - points[j];
					Vector3 start1 = points[j];
					Vector3 end1 = points[j+1];

					float totalParticleAmount = (float)(dif.magnitude / disFromParticle ) + leftOver;
					int particleAmount = (int)totalParticleAmount;
					leftOver = totalParticleAmount - particleAmount;

					Vector3 xDir = Vector3.Cross(dif, Vector3.up).normalized;
					Vector3 yDir = Vector3.Cross(dif, xDir).normalized;

					for(int i = 0; i < particleAmount; i++)
					{
						float disAlongPath = ((float)(total / (float)particlesCount));
						ParticleScript particle = ParticleScript.allParticles[total];

					Vector3 target = start1 + i * dif.normalized * disFromParticle + radius * Mathf.Sin(disAlongPath*frequency+offset) * xDir + radius * Mathf.Cos (disAlongPath*frequency+offset) * yDir;
					

						particle.transform.position = target;


						particle.headToPosition = true;
						
						particle.positionalTarget = target;
                      	total += 1;
					}
				}



			/*
				for(int i = 0; i < particlesCount; i++)
				{
					float disAlongPath = ((float)i / (float)particlesCount);
					ParticleScript particle = ParticleScript.allParticles[i];
					particle.headToPosition = true;
					particle.positionalTarget = origin + i * dir.normalized * realDisPerParticle + radius * Mathf.Sin(disAlongPath*frequency+offset) * xDir + radius * Mathf.Cos (disAlongPath*frequency+offset) * yDir;
				}*/

		}
	}

	public bool IsObjectInTractorBeam(GameObject go)
   	{
		Vector3 path = (end - origin).normalized;

		float originVal = Vector3.Project(origin-origin, path).magnitude;
		float endVal = Vector3.Project(end-origin, path).magnitude;

		Vector3 closestPoint = Vector3.Project(go.transform.position-origin, path);
		float closestPointVal = closestPoint.magnitude;

		//Check to see if the object is on the line
		if((closestPointVal > originVal && closestPointVal < endVal) || (closestPointVal < originVal && closestPointVal > endVal))
		{
			Vector3 dif = ((closestPoint+origin) - go.transform.position);
			if(dif.magnitude < tractorRadius)
			{
				return true;
			}
		}

		return false;
	}

	public Vector3 DifToTractorBeam(Vector3 point)
	{
		Vector3 path = (end - origin).normalized;
		
		float originVal = Vector3.Project(origin-origin, path).magnitude;
		float endVal = Vector3.Project(end-origin, path).magnitude;
		
		Vector3 closestPoint = Vector3.Project(point-origin, path);
		float closestPointVal = closestPoint.magnitude;
		
		//Check to see if the object is on the line
		if((closestPointVal > originVal && closestPointVal < endVal) || (closestPointVal < originVal && closestPointVal > endVal))
		{
			Vector3 dif = ((closestPoint+origin) - point);
			return dif;
		}
		else
		{
			Vector3 dif1 = point - origin;
			Vector3 dif2 = point - end;

			if(dif1.sqrMagnitude < dif2.sqrMagnitude)
			{
				return dif1;
			}
			else
			{
				return dif2;
			}
		}
	}
}
