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
    private bool m_airborn;

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
        //OnInput += InputGlobalGravity;
        OnInput += InputExternalGravityControl;

        m_subscriptions.Subscribe("input", OnInput);
    }

    private void Update()
    {
        //UpdateRigidPhysics();
        UpdateSelfTeather();
    }

    private void LateUpdate()
    {
        //ResetRigidhysics();
    }



    private void InputMove(Mouledoux.Callback.Packet packet)
    {
        Vector2 velocity = Vector2.zero;

        Vector2 leftStick = new Vector2(packet.floats[0], packet.floats[1]);
        bool buttonA = packet.bools[0];
        bool buttonB = packet.bools[1];

        velocity.x += leftStick.x * speed * (m_airborn ? 0.25f : 1f);
        velocity.y += buttonA && m_jumpsLeft > 0 ? m_jumpStrength :
            buttonB && m_jumpsLeft > 0 ? -m_jumpStrength *2f : -1;
        m_jumpsLeft -= buttonA || buttonB ? 1 : 0;

        velocity = transform.TransformDirection(velocity);
        velocity *= Utilities.timeScale;

        UpdateRigidPhysics();

        //m_rigidbody.AddForce(velocity);
        //m_rigidbody.AddForce(Utilities.globalGravity * Utilities.timeScale);

        ResetRigidhysics();
    }



    private void InputDirection(Mouledoux.Callback.Packet packet)
    {
        Vector2 direction = Camera.main.transform.up.normalized;
        transform.up = Vector3.Lerp(transform.up, direction, Time.deltaTime * m_speed);

    }



    private void InputTime(Mouledoux.Callback.Packet packet)
    {
        Utilities.timeScale = (packet.bools[0] ? 0.1f : 1.0f);
        
    }



    private void InputGlobalGravity(Mouledoux.Callback.Packet packet)
    {
        if(packet.bools[9])
        {
            Utilities.globalGravity = Vector3.zero;
            return;
        }
        else if(packet.floats[4] > 0.1f)
        {
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
        
        if(m_target != null)
        {
            m_target = null;
            return;
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
        if(m_target == null)
        {
            m_selfTeather.positionCount = 0;
            return;
        }

        m_selfTeather.SetPositions(new Vector3[] { transform.position, m_target.position });

        float dist = Vector3.Distance(transform.position, m_target.position);

        m_rigidbody.AddForce((m_target.position - m_rigidbody.position) * Utilities.timeScale * dist / 5f);
        m_target.AddForce((m_rigidbody.position - m_target.position) * Utilities.timeScale * dist / 5f);
    }



    private void UpdateRigidPhysics()
    {
        m_rigidbody.drag *= Utilities.timeScale;
        m_rigidbody.angularDrag *= Utilities.timeScale;
        m_rigidbody.gravityScale *= Utilities.timeScale;
    }

    private void ResetRigidhysics()
    {
        m_rigidbody.drag =
            m_rigidbody.drag != 0 ? m_rigidbody.drag / Utilities.timeScale : m_rigidbody.drag;
        m_rigidbody.angularDrag =
            m_rigidbody.angularDrag != 0 ? m_rigidbody.angularDrag / Utilities.timeScale : m_rigidbody.angularDrag;
        m_rigidbody.drag =
            m_rigidbody.gravityScale != 0 ? m_rigidbody.gravityScale / Utilities.timeScale : m_rigidbody.gravityScale;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_jumpsLeft = (Physics2D.Raycast(transform.position, -transform.up)) ? m_maxJumps : m_jumpsLeft;
        m_airborn = (m_jumpsLeft != m_maxJumps);

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        m_airborn = (collision.contacts.Length < 1);
    }
}