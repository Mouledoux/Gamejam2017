using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMotionController : MonoBehaviour
{
    private Vector3 m_localVelocity;
    public Vector3 localVelocity
    {
        get
        {
            return m_localVelocity;
        }
    }
    public Vector3 globalVelocity
    {
        get
        {
            return (transform.parent != null) ?
                transform.parent.GetComponent<RelativeMotionController>().globalVelocity + localVelocity :
                localVelocity;

        }
    }

    public bool m_useGravity;
    [SerializeField]
    private bool m_lockedInPlace;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void SetLocalVelocity(Vector3 newVel)
    {
        m_localVelocity = newVel;
    }
}
