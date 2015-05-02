﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ParticleCollector : MonoBehaviour
{

    private const int maxParticles = 10;

    private bool full = false;
    public int currentParticles = 0;

    public Animator anim;
    public GameObject animationTarget;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        LoopAnimation();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Particle" && !full)
        {
            //Debug.Log("Call Trigger Code");
            currentParticles++;
            col.gameObject.GetComponent<ParticleScript>().DestroyParticle();
            if (currentParticles >= maxParticles)
            {
                // Destroy(GetComponent<BoxCollider>()); // Might not wanna do this idk yet
                anim.SetBool("Full", true);
                animationTarget.GetComponent<FlowerHouse>().Activate();
                full = true;
            }
        }
    }

    private void LoopAnimation()
    {
   
    }
}
