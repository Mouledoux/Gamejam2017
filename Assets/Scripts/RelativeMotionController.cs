using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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

    [SerializeField] private float m_weight;
    [SerializeField] private float m_drag;

    [SerializeField] private float m_gravityMod;
    [SerializeField] private bool m_lockedInPlace;

    private Rigidbody m_rigidbody;

	void Start ()
    {
        RigidbodySetUp();
	}
	
	void Update ()
    {
        Move();
	}

    private int Move()
    {
        transform.Translate(m_localVelocity);

        return 0;
    }

    private int RigidbodySetUp()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        if (m_rigidbody == null) m_rigidbody = gameObject.AddComponent<Rigidbody>();
        m_rigidbody.isKinematic = true;

        return 0;
    }

    private void SetLocalVelocity(Vector3 newVel)
    {
        m_localVelocity = newVel;
    }

    
}
