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
    private Rigidbody2D m_target;

    private LineRenderer m_selfTeather;

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    Mouledoux.Callback.Callback OnInput;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_selfTeather = GetComponent<LineRenderer>();

        m_target = m_rigidbody;
        OnInput += InputMove;
        OnInput += InputDirection;
        OnInput += InputTime;
        OnInput += InputGlobalGravity;
        OnInput += InputExternalGravityControl;

        m_subscriptions.Subscribe("input", OnInput);
        Utilities.globalGravity = new Vector3(0, -9.8f, 0);
    }

    private void Update()
    {
        //GravityAdjust();
        UpdateSelfTeather();
    }



    private void InputMove(Mouledoux.Callback.Packet packet)
    {
        Vector2 velocity = Vector2.zero;

        Vector2 leftStick = new Vector2(packet.floats[0], packet.floats[1]);
        bool buttonA = packet.bools[0];
        bool buttonB = packet.bools[1];

        velocity.x += leftStick.x * speed;
        velocity.y += buttonA && m_jumpsLeft > 0 ? m_jumpStrength :
            buttonB && m_jumpsLeft > 0 ? -m_jumpStrength *2f : -1;
        m_jumpsLeft -= buttonA || buttonB ? 1 : 0;

        velocity *= Utilities.timeScale;

        velocity = transform.TransformDirection(velocity);

        m_rigidbody.AddForce(velocity);
        m_rigidbody.AddForce(Utilities.globalGravity);
    }



    private void InputDirection(Mouledoux.Callback.Packet packet)
    {
        Vector2 rightStick = new Vector2(packet.floats[2], packet.floats[3]);
        transform.right = Vector3.Lerp(transform.right, rightStick, Time.deltaTime * speed);
    }



    private void InputTime(Mouledoux.Callback.Packet packet)
    {
        Utilities.timeScale = 1f - Mathf.Clamp(packet.floats[4], 0f, .9f);
    }



    private void InputGlobalGravity(Mouledoux.Callback.Packet packet)
    {
        if(packet.bools[9])
        {
            Utilities.globalGravity = Vector3.zero;
            return;
        }

        Vector2 rightStick = new Vector2(packet.floats[2], packet.floats[3]);
        if (rightStick.magnitude > 0.01f)
        {
            Utilities.globalGravity += (Vector3)
                (rightStick *
                (10f + Utilities.globalGravity.magnitude)*
                Time.deltaTime);
        }
    }



    private void InputExternalGravityControl(Mouledoux.Callback.Packet packet)
    {
        if (!packet.bools[2] || packet.floats[4] < 0.1f) return;
        
        if(m_target != m_rigidbody)
        {
            m_target = m_rigidbody;
        }

        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, transform.right);
        if (rayHit.transform.GetComponent<Rigidbody2D>() == null) return;
        m_target = rayHit.transform.GetComponent<Rigidbody2D>();
    }



    private void GravityAdjust()
    {
        transform.up = Vector3.Lerp(transform.up, -Utilities.globalGravity, Time.deltaTime);
    }



    private void UpdateSelfTeather()
    {
        m_selfTeather.SetPositions(new Vector3[] { transform.position, m_target.position });

        float dist = Vector3.Distance(transform.position, m_target.position);

        m_rigidbody.AddForce((m_target.position - m_rigidbody.position) * dist / 5f);
        m_target.AddForce((m_rigidbody.position - m_target.position) * dist / 5f);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_jumpsLeft = (Physics2D.Raycast(transform.position, -transform.up)) ? m_maxJumps : m_jumpsLeft;

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}