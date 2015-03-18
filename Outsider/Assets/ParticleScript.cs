using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleScript : MonoBehaviour {

	public GameObject target;
	public GameObject secondaryTarget;

	SphereCollider collider;

    private GameObject player;
	public static List<GameObject> allParticles = new List<GameObject>();

	void Awake()
	{
		allParticles.Add(this.gameObject);
	}
	// Use this for initialization
	void Start () {

		collider = GetComponent<SphereCollider>();
        player = target;

        RandomizeParticleTargets();
	}

	public float gravity = 5f;
	public float repelForce = 50f;

	public float targetAcceleration = 20f;

	public float maxSpeed = 30f;

	Vector3 velocity;

	// Update is called once per frame
	void Update () {

		if(velocity.magnitude > maxSpeed)
		{
			velocity = maxSpeed * velocity.normalized;
		}

		transform.position += velocity * Time.deltaTime;
		velocity += new Vector3(0,-gravity,0f) * Time.deltaTime;

		if(target != null)
		{
			velocity += (target.transform.position - transform.position).normalized * targetAcceleration * Time.deltaTime;
		}

        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            // To Fix: Buggyness when switching targets.
            // secondaryTarget = player.RaycastOut();
            target = secondaryTarget;
        }*/

	}

    public void RandomizeParticleTargets()
    {
        if (Random.value > .6f)
        {
            target = allParticles[Random.Range(0, allParticles.Count)];
            secondaryTarget = allParticles[Random.Range(0, allParticles.Count)];
        }
    }

    public void SetTarget(GameObject newTarget) 
    {
        target = newTarget;
        RandomizeParticleTargets();
    }

    public void SetTargetToPlayer()
    {
        SetTarget(player);
    }

	void OnTriggerStay(Collider other) {

		Vector3 pointOfContact = other.ClosestPointOnBounds(transform.position);
		Vector3 dif = pointOfContact - transform.position;

		velocity -= ((1f - ((dif).magnitude / collider.radius)) * Time.deltaTime * repelForce) * dif.normalized;
	}
}
