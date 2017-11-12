using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RelativeMotionController2D
{ 
    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions =
        new Mouledoux.Components.Mediator.Subscriptions();

    Mouledoux.Callback.Callback OnInputMove;

    private new void Start()
    {
        base.Start();
        OnInputMove += InputMove;
        m_subscriptions.Subscribe("input", OnInputMove);
    }

    private void InputMove(Mouledoux.Callback.Packet packet)
    {
        Vector2 leftStick, rightStick;
        leftStick = rightStick = Vector2.zero;

        Vector2 leftTrigger, rightTrigger;
        leftTrigger = rightTrigger = Vector2.zero;

        bool buttonA, buttonB, buttonX, buttonY;
        buttonA = buttonB = buttonX = buttonY = false;

        bool dPadUp, dPadDown, dPadLeft, dPadRight;
        dPadUp = dPadDown = dPadLeft = dPadRight = false
            ;
        bool leftBumber, rightBumber;
        leftBumber = rightBumber = false;

        leftStick.x = packet.floats[0];
        leftStick.y = packet.floats[1];
        rightStick.x = packet.floats[2];
        rightStick.y = packet.floats[3];

        SetLocalVelocity(leftStick);
    }
}