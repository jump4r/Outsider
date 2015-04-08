using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

    private float oscSpeed = .1f;
    private float oscLength = .05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * oscSpeed) * oscLength), transform.position.z);
	}
}
