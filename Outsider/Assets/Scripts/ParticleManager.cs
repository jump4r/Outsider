using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

	public static List<GameObject> AllParticles;

	// Manage particle presentation
	private static float maxFlyTime = 5f;
	public static float currentFlyTime = 0f;
	private static int numParticles = 0;
	private int numActiveParticles = 0;

	// Use this for initialization
	void Start () {
		//AllParticles = ParticleScript.allParticles;
		numParticles = ParticleScript.allParticles.Count;
		maxFlyTime = numParticles * 0.025f;
		Debug.Log ("Max Fly Time: " + maxFlyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private static void UpdateFlyLength() {
		maxFlyTime = numParticles * 0.25f;
	}

	public static bool CanFly() {
		if (currentFlyTime > maxFlyTime) {
			//Debug.Log ("Cannot fly");
			return false;
		}
		return true;
	}

	public static void IncrementFlyTime() {
		currentFlyTime += Time.fixedDeltaTime;
	}

	public static void ResetFlyTime() {
		currentFlyTime = 0f;
	}
}
