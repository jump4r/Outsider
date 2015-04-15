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

	public bool yOnly;

	void Update()
	{
		if(yOnly)
		{
			Vector3 v = m_Camera.transform.position - transform.position;
			v.x = v.z = 0.0f;
			transform.LookAt(m_Camera.transform.position - v); 
		}
		else
		{
			transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.back,
			                 m_Camera.transform.rotation * Vector3.up);
		}
	}
}