using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RelativeMotionController2D : MonoBehaviour
{
    private Vector2 m_localVelocity;
    public Vector2 localVelocity
    {
        get
        {
            return m_localVelocity;
        }
    }
    public Vector2 globalVelocity
    {
        get
        {
            return (transform.parent != null) ?
                transform.parent.GetComponent<RelativeMotionController2D>().
                globalVelocity + localVelocity :
                localVelocity;

        }
    }

    private Vector2 m_localAngularVelocity;
    public Vector2 localAngularVelocity
    {
        get
        {
            return m_localAngularVelocity;
        }
    }
    public Vector2 globalAngularVelocity
    {
        get
        {
            return (transform.parent != null) ?
                transform.parent.GetComponent<RelativeMotionController2D>().
                globalAngularVelocity + localAngularVelocity :
                localAngularVelocity;

        }
    }

    public float m_speed;

    [SerializeField] private float m_weight;
    [SerializeField, Range(0f, 1f)] private float m_drag;

    [SerializeField] private float m_gravityMod;
    [SerializeField] private bool m_lockedInPlace;

    private Rigidbody2D m_rigidbody;

    protected void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        if (m_lockedInPlace) return;

        CheckGravity();
        Move();

        SetLocalVelocity(m_localVelocity - (m_localVelocity * m_drag));
    }

    protected int Move()
    {
        m_rigidbody.velocity = m_localVelocity;
        return 0;
    }

    protected void CheckGravity()
    {
        Vector2 gravity = new Vector2(0, Time.deltaTime * -m_gravityMod);
        IncreaseLocalVelocity(gravity);
    }

    protected void SetLocalVelocity(Vector2 newVel)
    {
        m_localVelocity = newVel;
    }

    protected void IncreaseLocalVelocity(Vector2 vel)
    {
        SetLocalVelocity(m_localVelocity + vel);
    }
}
