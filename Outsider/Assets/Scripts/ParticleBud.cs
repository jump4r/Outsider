using UnityEngine;
using System.Collections;

public class ParticleBud : MonoBehaviour {

	// [RequireComponent (typeof (Animator))]

	public int  numToSpawn = 4;
	public GameObject particle;
	private const float particleExitVelocity = 11f; 
	private bool spawnFlag = true;

	public AnimationClip open;
	private Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnParticles() {
		for ( int i = 0; i < numToSpawn; i++) {
			GameObject tmp = GameObject.Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
			tmp.GetComponent<ParticleScript>().SetVelocity ((Vector3.up + (Random.insideUnitSphere) * .3f) * particleExitVelocity);
			spawnFlag = false;

            
			Debug.Log ("Play the god damn door opening animation");
			anim.clip = open;
			anim.Play ();
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" && spawnFlag) {
			SpawnParticles ();
		}
	}
}
