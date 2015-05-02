using UnityEngine;
using System.Collections;

public class KeyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    Vector3 rotationVel;
    Vector3 rotationAccel;
    
	// Update is called once per frame
	void Update () {
        rotationAccel += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rotationAccel.Normalize();
        rotationVel += rotationAccel * Time.deltaTime;
        rotationVel.Normalize();
        transform.Rotate(rotationVel);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            EndScript.instance.EndCondition += 1;
            Destroy(this.gameObject);
        }
    }
}
