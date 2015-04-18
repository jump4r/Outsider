using UnityEngine;
using System.Collections;

public class FogZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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
			RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, fogColor, rate * Time.deltaTime);
		}
	}
}
