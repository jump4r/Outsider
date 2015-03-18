using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject newTarget = RaycastOut();
            if (newTarget == null)
            {
                newTarget = this.gameObject;
            }

            for (int i = 0; i < ParticleScript.allParticles.Count; i++) 
            {
                ParticleScript.allParticles[i].GetComponent<ParticleScript>().SetTarget(newTarget);
            }
        }
	}

    public GameObject RaycastOut()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log("Object Hit: " + hit.collider.name);
            return hit.collider.gameObject;
        }

        else 
        {
            return null;
        }
    }
}
