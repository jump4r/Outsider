using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{

    public GameObject doorObject;

    private Door door;
    private static int openThreshold = 50;
    private int numParticles = 0;

    // Use this for initialization
    void Start()
    {
        door = doorObject.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Note, this key does not need to be a collectable,
    // When we figure out how we actually want to unlock the doors the code will go here
    // Until then I'm going to use OnTriggerEnter.
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            door.unlocked = true;
            Debug.Log("Door Unlocked");
            Destroy(this.gameObject);
        }

        // If we have particles opening the door.
        else if (col.tag == "Particle")
        {
            if (numParticles >= openThreshold)
            {
                door.unlocked = true;
                door.OpenDoor();
                // Before Destory, set particles back to original target, need access to ParticleScript.cs to do this.
                foreach (GameObject p in ParticleScript.allParticles)
                {
                    p.GetComponent<ParticleScript>().SetTargetToPlayer();
                }
                Destroy(this.gameObject);
            }

            else
            {
                numParticles += 1;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Particle")
        {
            numParticles -= 1;
        }
    }
}
