using UnityEngine;
using System.Collections;

public class ParticleBud : MonoBehaviour {

	// [RequireComponent (typeof (Animator))]

	[SerializeField] private AudioClip m_creakSound;

	public int  numToSpawn = 4;

	public GameObject particle;
    public GameObject soundParticle;
    private bool spawnRandomSoundParticle = false;

	private const float particleExitVelocity = 11f; 
	private bool spawnFlag = true;

	public AnimationClip open;
	private Animation anim;
	private AudioSource m_audioSource;

    public Sprite[] possibleSprites;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		m_audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void SpawnParticles() {

        int randomSprite = Random.Range(0, possibleSprites.Length);

		for ( int i = 0; i < numToSpawn; i++) {
            if (!spawnRandomSoundParticle)
            {
                GameObject sParticle = GameObject.Instantiate(soundParticle, transform.position, Quaternion.identity) as GameObject;
                sParticle.GetComponent<ParticleScript>().SetVelocity((Vector3.up + (Random.insideUnitSphere) * .3f) * particleExitVelocity);
                sParticle.GetComponent<SpriteRenderer>().sprite = possibleSprites[randomSprite];
                spawnFlag = false;
                spawnRandomSoundParticle = true;
                continue;
            }

			GameObject tmp = GameObject.Instantiate(particle, transform.position, Quaternion.identity) as GameObject;
			tmp.GetComponent<ParticleScript>().SetVelocity ((Vector3.up + (Random.insideUnitSphere) * .3f) * particleExitVelocity);
            tmp.GetComponent<SpriteRenderer>().sprite = possibleSprites[randomSprite];
            spawnFlag = false;

            
			Debug.Log ("Play the god damn door opening animation");
			anim.clip = open;
			anim.Play ();
			PlayOpenSound ();

		}
	}

	void PlayOpenSound() {
		m_audioSource.clip = m_creakSound;
		m_audioSource.Play ();
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player" && spawnFlag) {
			SpawnParticles ();
		}
	}
}
