using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
	public Camera m_Camera;

	void  Awake ()
	{
		// if no camera referenced, grab the main camera
		if (!m_Camera)
			m_Camera = Camera.main; 
	}

	void Update()
	{
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back,
		                 m_Camera.transform.rotation * Vector3.up);
	}
}