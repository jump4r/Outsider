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

	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButtonDown(0))
		{
			if(tractorBeamOn)
			{
				tractorBeamOn = false;
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
			radius = 1f + Mathf.Sin (offset/20f)* .5f;

			int particlesCount = ParticleScript.allParticles.Count;

			Vector3 dir = end - origin;
			Vector3 xDir = Vector3.Cross(dir, Vector3.up).normalized;
			Vector3 yDir = Vector3.Cross(dir, xDir).normalized;

			for(int i = 0; i < particlesCount; i++)
			{
				float disAlongPath = ((float)i / (float)particlesCount);

				ParticleScript.allParticles[i].transform.position = origin + i * dir.normalized * disFromParticle + radius * Mathf.Sin(disAlongPath*frequency+offset) * xDir + radius * Mathf.Cos (disAlongPath*frequency+offset) * yDir;
			}
		}
	}
}
