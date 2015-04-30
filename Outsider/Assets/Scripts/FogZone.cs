using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FogZone : MonoBehaviour {

    Camera[] cameras;
	// Use this for initialization
	void Start () {
        cameras = GameObject.FindObjectsOfType<Camera>();
	}

	static FogZone currentFogZone;

	public float fogEndDistance = 300f;
	public float fogStartDistance = 100f;

	public Color fogColor = Color.blue;

	public float rate = .1f;

	void OnTriggerEnter(Collider other) {

		if(other.tag == "Player")
		{
			if(currentFogZone != null)
			{
			//	currentFogZone.enabled = false;
			}

			currentFogZone = this;
			//this.enabled = true;
		}
	}

	void Update()
	{
		if(currentFogZone == this)
		{
			RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, fogStartDistance, rate * Time.deltaTime);
			RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, fogEndDistance, rate * Time.deltaTime);

            Color c = Color.Lerp(RenderSettings.fogColor, fogColor, rate * Time.deltaTime);
            RenderSettings.fogColor = c;

            foreach (Camera cam in cameras)
            {
                cam.backgroundColor = c;
            }
		}
	}
}
