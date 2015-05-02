using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController))]
public class Player : MonoBehaviour {

	private CharacterController m_CharacterController;
	private bool m_PreviouslyGrounded;
	private float flightAcceleration = .5f;
	private float flightVelocity = 2f;
	private const float maxFlightAcceleration = 5f;
    private Rigidbody r;

    public bool OculusEnabled;
    public GameObject TractorBeamObject;
    private TractorBeamScript tractorBeam;
	// Use this for initialization
	void Start () {
		m_CharacterController = GetComponent<CharacterController>();
        tractorBeam = TractorBeamObject.GetComponent<TractorBeamScript>();
        r = GetComponent<Rigidbody>();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject newTarget = RaycastOut();
            if (newTarget == null)
            {
                newTarget = this.gameObject;
            }

            for (int i = 0; i < ParticleScript.allParticles.Count; i++) 
            {
                ParticleScript.allParticles[i].GetComponent<ParticleScript>().SetTarget(newTarget);
            }
        }

        if (Input.GetButtonDown("Jump") && OculusEnabled && tractorBeam.tractorBeamOn)
        {
            r.AddForce(Vector3.up * 10, ForceMode.Acceleration);
        }
	}

	void FixedUpdate() {
		// Flight Manager
		if (Input.GetKey (KeyCode.Space)) {
			//Fly ();
		}
		
		m_PreviouslyGrounded = m_CharacterController.isGrounded;
	}

	/// <summary>
	/// To the damn sky.
	/// </summary>
	private void Fly() {
		Debug.Log ("Character Fly");
		Vector3 newPos = Vector3.zero;
		newPos.y += Time.fixedDeltaTime * flightVelocity;
		m_CharacterController.Move(newPos);
	}

    private GameObject RaycastOut()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log("Object Hit: " + hit.collider.name);
            return hit.collider.gameObject;
        }

        else 
        {
            return null;
        }
    }
}
