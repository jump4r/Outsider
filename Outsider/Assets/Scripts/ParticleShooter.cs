﻿using UnityEngine;
using System.Collections;

public class ParticleShooter : MonoBehaviour {

    private const float shootSpeed = 5f;
    private int particleIndex = 0;
    private Camera camera;

	// Use this for initialization
	void Start () {
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            
            if (Physics.Raycast(ray, out hit, 100f))
            {
                Shoot(hit.transform);
            }
        }
	}

    public void Shoot(Transform target) { 
        if (particleIndex >= ParticleScript.allParticles.Count) {
            particleIndex = 0;
        }

        ParticleScript ps = ParticleScript.allParticles[particleIndex].GetComponent<ParticleScript>();
        ps.shot = true;
        ps.velocity = (target.transform.position - ps.gameObject.transform.position).normalized * shootSpeed * Time.fixedDeltaTime;
        ps.SetShotTarget(target.gameObject);

        
        Debug.Log("Shoot Particle: " + particleIndex + " at GameObject " + target.transform);
        particleIndex += 1;
    }
}
