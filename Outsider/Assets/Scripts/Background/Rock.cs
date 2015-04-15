using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour
{

    private float oscSpeed = .3f;
    private float oscSpeed2 = .6f;
    private float oscLength = 50;

    private bool upDirection = true;
    private Vector3 startPos;
    private Vector3 endPos;

    // Use this for initialization
    void Start()
    {
        oscSpeed = Random.Range(.2f, .4f);
        oscSpeed2 = Random.Range(.3f, .6f);
        oscLength = Random.Range(20, 50);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * oscSpeed) * oscSpeed2) / oscLength, transform.position.z);
        //transform.position = (upDirection) ? Vector3.Lerp(startPos, endPos, 5f) : Vector3.Lerp(endPos, startPos, 5f);

    }
}