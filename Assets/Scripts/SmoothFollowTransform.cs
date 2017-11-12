using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowTransform : MonoBehaviour
{
    public Transform m_target;
    public Vector3 m_offset;
    public float m_followSpeed;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position =
            Vector3.LerpUnclamped(transform.position, m_target.position + m_offset, Time.deltaTime * m_followSpeed);
	}
    
}
