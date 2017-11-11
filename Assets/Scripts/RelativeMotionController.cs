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

    [SerializeField] private float m_weight;
    [SerializeField] private float m_drag;

    [SerializeField] private float m_gravityMod;
    [SerializeField] private bool m_lockedInPlace;

    private Rigidbody2D m_rigidbody;

    private void Start ()
    {
        RigidbodySetUp();
	}

    private void Update ()
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
        m_rigidbody = GetComponent<Rigidbody2D>();
        if (m_rigidbody == null) m_rigidbody = gameObject.AddComponent<Rigidbody2D>();
        m_rigidbody.isKinematic = true;

        return 0;
    }

    private void SetLocalVelocity(Vector3 newVel)
    {
        m_localVelocity = newVel;
    }

    
}
