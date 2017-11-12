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
    [SerializeField] private float m_drag;

    [SerializeField] private float m_gravityMod;
    [SerializeField] private bool m_lockedInPlace;

    protected void Start()
    {
    }

    protected void Update()
    {
        Move();
    }

    protected int Move()
    {
        float speed = Time.deltaTime * m_speed;
        transform.Translate(m_localVelocity * speed);
        return 0;
    }

    protected void SetLocalVelocity(Vector2 newVel)
    {
        m_localVelocity = newVel;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Vector2 averageCollisionPoint = Vector2.zero;
        foreach(ContactPoint point in collision.contacts)
        {
            averageCollisionPoint += (Vector2)point.point;
        }

        print(collision.collider.name);
    }
}
