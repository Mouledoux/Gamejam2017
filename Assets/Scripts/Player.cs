using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_speed;
    public float speed
    {
        get { return m_speed * Time.deltaTime * Utilities.timeScale; }
    }
    public float m_jumpStrength;
    [SerializeField]
    private int m_maxJumps;
    private int m_jumpsLeft;

    private Rigidbody2D m_rigidbody;

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    Mouledoux.Callback.Callback OnInputMove, OnInputTime;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        OnInputMove += InputMove;
        OnInputTime += InputTime;
        m_subscriptions.Subscribe("input", OnInputMove);
        m_subscriptions.Subscribe("input", OnInputTime);
    }


    private void InputMove(Mouledoux.Callback.Packet packet)
    {
        Vector2 velocity = Vector2.zero;

        Vector2 leftStick = new Vector2(packet.floats[0], packet.floats[1]);
        bool buttonA = packet.bools[0];
        
        velocity.x = leftStick.x * speed;
        velocity.y = buttonA && m_jumpsLeft > 0 ? m_jumpStrength : -9.8f;
        m_jumpsLeft -= buttonA ? 1 : 0;

        velocity *= Utilities.timeScale;

        m_rigidbody.AddForce(velocity);
    }


    private void InputTime(Mouledoux.Callback.Packet packet)
    {
        float leftTrigger, rightTrigger;
        leftTrigger = rightTrigger = 0f;

        leftTrigger = packet.floats[4];
        rightTrigger = packet.floats[5];

        Utilities.timeScale = 1f - Mathf.Clamp(leftTrigger, 0f, .9f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_jumpsLeft = (collision.contacts[0].point.y < transform.position.y) ? m_maxJumps : m_jumpsLeft;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
    }
}