using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	SphereCollider collider;
	// Use this for initialization
	void Start () {
		collider = GetComponent<SphereCollider>();
	}

	public float gravity = 5f;
	public float repelForce = 50f;

	Vector3 velocity;

	// Update is called once per frame
	void Update () {
		transform.position += velocity * Time.deltaTime;
		velocity += new Vector3(0,-gravity,0f) * Time.deltaTime;
	}

	void OnTriggerStay(Collider other) {

		Vector3 pointOfContact = other.ClosestPointOnBounds(transform.position);
		Vector3 dif = pointOfContact - transform.position;

		velocity -= ((1f - ((dif).magnitude / collider.radius)) * Time.deltaTime * repelForce) * dif.normalized;
	}
}
