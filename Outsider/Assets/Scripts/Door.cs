using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{

    [HideInInspector]
    public bool unlocked = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OpenDoor()
    {
        if (unlocked)
        {
            Debug.Log("Play Door Opening Animation.");
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void CloseDoor()
    {
        if (unlocked)
        {
            Debug.Log("Play Door Closing Animation.");
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
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
