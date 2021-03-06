﻿using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour
{
    public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.

    public int EndCondition = 0;         // Mini-end condition if we want to add it, collect all (3/4?) Keys to activate end condition.

    private bool sceneStarting = true;      // Whether or not the scene is still fading in.

    public GameObject endCamera;

    public static EndScript instance;
    GUITexture guiTexture;
    void Awake()
    {
        instance = this;
        guiTexture = GetComponent<GUITexture>();
        // Set the texture so that it is the the size of the screen and covers it.
         guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }


    void Update()
    {
        // If the scene is starting...
        if (sceneStarting)
            // ... call the StartScene function.
            StartScene();

        if(endingScene)
        {
            EndScene();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            endingScene = true;
        }
    }


    void FadeToClear()
    {
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    float rate = 3f;
    Color fogColor = Color.white;

    float count = 0f;

    void FadeToBlack()
    {
        // Lerp the colour of the texture between itself and black.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);

        RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, -200, rate * Time.deltaTime);
        RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 0, rate * Time.deltaTime);

        Color c = Color.Lerp(RenderSettings.fogColor, fogColor, rate * Time.deltaTime);
    }

    void FadeFromBlack()
    {
        guiTexture.color = Color.Lerp(Color.black, guiTexture.color, fadeSpeed * Time.deltaTime);

        RenderSettings.fogStartDistance = Mathf.Lerp(RenderSettings.fogStartDistance, 0, rate * Time.deltaTime);
        RenderSettings.fogEndDistance = Mathf.Lerp(RenderSettings.fogEndDistance, 10, rate * Time.deltaTime);

        Color c = Color.Lerp(RenderSettings.fogColor, Color.white, rate * Time.deltaTime);
    }

    void StartScene()
    {
        // Fade the texture to clear.
        FadeToClear();

        // If the texture is almost clear...
        if (guiTexture.color.a <= 0.05f)
        {
            // ... set the colour to clear and disable the GUITexture.
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }

    bool endingScene = false;

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && EndCondition > 4)
        {
            endingScene = true;
            
        }
    }

    bool secondCam = false;
    public void EndScene()
    {
        // Make sure the texture is enabled.
        guiTexture.enabled = true;

        // Start fading towards black.
        FadeToBlack();

        count += Time.deltaTime;
        // If the screen is almost black...
        if (count > rate && !secondCam)
        {
            secondCam = true;
            Camera[] cameras = GameObject.FindObjectsOfType<Camera>();
            foreach(Camera c in cameras)
            {
                c.enabled = false;
            }

            endCamera.SetActive(true);
        }

        if(secondCam)
        {
            FadeFromBlack();
        }
            // ... reload the level.
           // Application.LoadLevel(0);
    }
}