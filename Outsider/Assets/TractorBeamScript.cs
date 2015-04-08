using UnityEngine;
using System.Collections;

public class TractorBeamScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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

	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0))
		{
			if(tractorBeamOn)
			{
				tractorBeamOn = false;
				int particlesCount = ParticleScript.allParticles.Count;
				for(int i = 0; i < particlesCount; i++)
				{
					ParticleScript particle = ParticleScript.allParticles[i];
					particle.headToPosition = false;
				}
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
			}

		}

		if(tractorBeamOn)
		{
			offset -= updateSpeed * Time.deltaTime;

			frequency = .5f + Mathf.Sin (offset/20f)* 100f;
			radius = 1.5f + Mathf.Sin (offset/20f)* .5f;

			int particlesCount = ParticleScript.allParticles.Count;

			Vector3 dir = end - origin;
			Vector3 xDir = Vector3.Cross(dir, Vector3.up).normalized;
			Vector3 yDir = Vector3.Cross(dir, xDir).normalized;

			float realDisPerParticle = (end - origin).magnitude / (float)particlesCount;
		
			for(int i = 0; i < particlesCount; i++)
			{
				float disAlongPath = ((float)i / (float)particlesCount);
				ParticleScript particle = ParticleScript.allParticles[i];
				particle.headToPosition = true;
				particle.positionalTarget = origin + i * dir.normalized * realDisPerParticle + radius * Mathf.Sin(disAlongPath*frequency+offset) * xDir + radius * Mathf.Cos (disAlongPath*frequency+offset) * yDir;
			}

			if(IsObjectInTractorBeam(gameObjectToFloat))
			{
				gameObjectToFloat.GetComponent<CharacterController>().Move((end - origin).normalized*100f * Time.deltaTime);//.AddForce((origin - end).normalized * 30f);
			}

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
}
