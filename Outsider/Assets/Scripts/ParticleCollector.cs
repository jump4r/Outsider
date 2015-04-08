﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ParticleCollector : MonoBehaviour
{

    private const int maxParticles = 10;

    private bool full = false;
    public int currentParticles = 0;
    public Material fullMaterial;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Trigger Entered: " + col.tag);
        if (col.tag == "Particle" && !full)
        {
            Debug.Log("Call Trigger Code");
            currentParticles++;
            Destroy(col.gameObject);
            if (currentParticles >= maxParticles)
            {
                // Destroy(GetComponent<BoxCollider>()); // Might not wanna do this idk yet
                full = true;
                GetComponent<MeshRenderer>().material = fullMaterial;
            }
        }
    }

}
