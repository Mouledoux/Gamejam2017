using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{

    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    Mouledoux.Callback.Callback OnInput;

    private void Start()
    {
        OnInput += InputDirection;

        m_subscriptions.Subscribe("input", OnInput);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void InputDirection(Mouledoux.Callback.Packet packet)
    {
        float direction = packet.floats[4] > 0.5f ? packet.floats[4] : 0f;
        direction += packet.floats[5] < -0.5f ? packet.floats[4] : 0f;

        transform.RotateAround(transform.position, transform.forward, -direction * 4f);

        Physics2D.gravity = -transform.up * 10f;
    }
}
