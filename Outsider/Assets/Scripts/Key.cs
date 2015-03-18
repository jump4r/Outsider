using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour
{

    public GameObject doorObject;
    private Door door;

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
            Destroy(gameObject);
        }
    }
}
