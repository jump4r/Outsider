using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {


	bool paused;

	float blurPercent = 0f;
	float blurTarget = 3.5f;
	public float blurSpeed = .4f;
	Camera cam;

	UnityStandardAssets.ImageEffects.BlurOptimized blur;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		blur = cam.GetComponent<UnityStandardAssets.ImageEffects.BlurOptimized>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			paused = !paused;

			if(paused)
			{
				Time.timeScale = 0;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				Time.timeScale = 1;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}
		}

		if(paused)
		{
			blur.enabled = true;
			blurPercent += 1f/60f * blurSpeed;

			if(blurPercent > 1f)
			{
				blurPercent = 1f;
			}

			blur.blurSize = blurPercent * blurTarget;
		}
		else
		{
			blurPercent -= 3f/60f * blurSpeed;
			
			if(blurPercent < 0f)
			{
				blurPercent = 0f;
				blur.enabled = false;
			}

			blur.blurSize = blurPercent * blurTarget;
		}
	}
}
