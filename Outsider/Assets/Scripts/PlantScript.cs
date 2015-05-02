using UnityEngine;
using System.Collections;

public class PlantScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.parent = null;
		//this.transform.localScale = this.transform.localScale + new Vector3(Random.value * .2f, Random.value * .7f, Random.value * .2f);
	}

	public float ScaleVariation;

	// Update is called once per frame
	void Update () {
	
	}
}
