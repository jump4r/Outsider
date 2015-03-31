using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

	public static List<GameObject> AllParticles;

	// Manage particle presentation
	private float maxFlyTime = 0f;
	private float currentFlyTime = 0f;
	private int numParticles = 0;
	private int numActiveParticles = 0;

	// Use this for initialization
	void Start () {
		//AllParticles = ParticleScript.allParticles;
		numParticles = ParticleScript.allParticles.Count;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void UpdateFlyLength() {
		maxFlyTime = numParticles * 0.25f;
	}

	private bool CanFly() {
		if (currentFlyTime > maxFlyTime) {
			return false;
		}

		return true;
	}
}
