using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

    public bool OculusEnabled;

    public GameObject OculusCamera;
    public GameObject NormalCamera;

	// Use this for initialization
	void Start () {
        if (OculusEnabled)
        {
            OculusCamera.active = true;
            NormalCamera.active = false;
        }

        else
        {
            NormalCamera.active = true;
            OculusCamera.active = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        {
            if (OculusEnabled)
            {
                Application.LoadLevel("test2-Oculus");
            }

            else {
                Application.LoadLevel("test2");
            }
        }
	}
}
