using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class FlowerHouse : MonoBehaviour
{

    private Animator animator;
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: Inherit 'Activatable' class so I can just just call Activate from some static function.
    public void Activate()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        animator.SetTrigger("OpenTrigger");
    }

    void OnTriggerEnter(Collider col)
    {

    }
}
