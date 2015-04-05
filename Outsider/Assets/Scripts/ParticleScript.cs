using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleScript : MonoBehaviour {

	public GameObject target;
	public GameObject secondaryTarget;

	SphereCollider collider;

    private static GameObject player;
	public static List<GameObject> allParticles = new List<GameObject>();

    public bool shot = false;
    private const float shotVelocityMult = 20f;

	private static Vector3 wave;
	private static float waveMagnitude = 1f;
	private static float waveDuration = 1f;
	private bool myWave = false;


	void Awake()
	{
		allParticles.Add(this.gameObject);

		if(player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player") as GameObject;
		}

		wave = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
		myWave = true;
		waveMagnitude = .2f;
		waveDuration = 2f;
	}

	// Use this for initialization
	void Start () {

		collider = GetComponent<SphereCollider>();

        RandomizeParticleTargets();
	}

	public float gravity = 5f;
	public float repelForce = 50f;

	public float targetAcceleration = 20f;

	public float maxSpeed = 30f;

	public Vector3 velocity;

	// Update is called once per frame
	void Update () {

		if(myWave)
		{
			wave = new Vector3(wave.x, wave.y + Time.deltaTime, wave.z);
			waveDuration -= Time.deltaTime;
			if(waveDuration < 0f)
			{
				waveDuration = 0f;
			}
		}

        Vector3 dif = (target.transform.position - transform.position);

        float maxSpeedReal = maxSpeed + Mathf.Min(dif.magnitude, 2f);

        float accelerationReal = maxSpeedReal / maxSpeed * targetAcceleration; //Scale acceleration the same amount


        if (velocity.magnitude > maxSpeedReal & !shot)
        {
            velocity = maxSpeedReal * velocity.normalized;
        }

        transform.position += velocity * Time.deltaTime;

        velocity += new Vector3(0, -gravity, 0f) * Time.deltaTime;

        Vector2 waveDif = new Vector2(transform.position.x, transform.position.y) - new Vector2(wave.x, wave.y);

        if (waveDuration > 0f)
        {
            velocity += Vector3.up * (waveDif.magnitude / 3f) * waveMagnitude * Time.deltaTime;
        }

        if (shot)
        {
            velocity = (target.transform.position - transform.position).normalized * shotVelocityMult * Time.fixedDeltaTime;
        }

        if (target != null)
        {
            if (dif.magnitude > 6f)
            {
                velocity += dif.normalized * accelerationReal * Time.deltaTime;
            }
            else
            {
                velocity -= velocity.normalized * velocity.magnitude * .8f * Time.deltaTime;
            }

            if (shot)
            {
                velocity *= shotVelocityMult;
            }

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
		else
		{
			target = player;
		}
    }

	public void SetVelocity(Vector3 newVel) {
		velocity = newVel;
	}

    public void SetShotTarget(GameObject newTarget)
    {
        target = newTarget;
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
