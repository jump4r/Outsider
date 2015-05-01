using UnityEngine;
using System.Collections;

public class ParticleShooter : MonoBehaviour {

    private const float shootSpeed = 5f;
    private int particleIndex = 0;
    private Camera camera;

	// Use this for initialization
	void Start () {
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = new Ray(transform.position, this.transform.forward);
            RaycastHit[] hit = Physics.RaycastAll(transform.position, transform.forward, 100f);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);  
            bool collectorDetected = false;

            // if (Physics.RaycastAll(ray, out hit, ))
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.tag == "Collector")
                {
                    Shoot(hit[i].transform);
                    break;
                }
            }

            // Hacky solution to simulate shooting because raycasting doesn't work /cry.
            // Shoot first object that was hit.
            /*
            if (!collectorDetected && hit.Length > 0)
            {
                Shoot(hit[0].transform);
            }*/
        }
	}

    public void Shoot(Transform target) {

        if (ParticleScript.allParticles.Count <= 0)
        {
            Debug.Log("Don't shoot, no particles");
            return;
        }

        if (particleIndex >= ParticleScript.allParticles.Count) {
            particleIndex = 0;
        }

        ParticleScript ps = ParticleScript.allParticles[particleIndex].GetComponent<ParticleScript>();
        ps.shot = true;
        ps.velocity = (target.transform.position - ps.gameObject.transform.position).normalized * shootSpeed * Time.fixedDeltaTime;
        ps.SetShotTarget(target.gameObject);
        ps.Invoke("SetTargetToPlayer", 5f);
        
        Debug.Log("Shoot Particle: " + particleIndex + " at GameObject " + target.transform);
        particleIndex += 1;
    }
}
