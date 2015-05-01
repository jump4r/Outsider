using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Animator))]

public class Door : MonoBehaviour
{

    public bool unlocked = false;
    private Animator animator;
    public GameObject meshCollider_object;
    private MeshCollider meshCollider;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        meshCollider = meshCollider_object.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        if (unlocked)
        {
            Debug.Log("Play Door Opening Animation.");
            animator.SetBool("Entered", true);
            animator.SetBool("Exited", false);
            meshCollider.enabled = false;
        }
    }

    public void CloseDoor()
    {
        if (unlocked)
        {
            Debug.Log("Play Door Closing Animation.");
            animator.SetBool("Entered", false);
            animator.SetBool("Exited", true);
            meshCollider.enabled = true;
        }
    }

    // Again, not totally sure how doors will be opening, but I will use triggers until
    // we figure out what we're doing.
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            OpenDoor();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            CloseDoor();
        }
    }
}
