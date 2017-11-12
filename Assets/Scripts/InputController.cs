using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private static InputController m_instance;
    public InputController _instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<InputController>();
            }

            if(m_instance != this)
            {
                Destroy(gameObject);
            }

            return m_instance;
        }
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Vector2 leftStick, rightStick;
        leftStick = rightStick = Vector2.zero;

        Vector2 leftTrigger, rightTrigger;
        leftTrigger = rightTrigger = Vector2.zero;

        bool buttonA, buttonB, buttonX, buttonY;
        buttonA = buttonB = buttonX = buttonY = false;

        bool dPadUp, dPadDown, dPadLeft, dPadRight;
        dPadUp = dPadDown = dPadLeft = dPadRight = false;

        bool leftBumber, rightBumber;
        leftBumber = rightBumber = false;



        bool[] buttons = new bool[16];
        float[] joysticks = new float[6];


        leftStick = GetJoystickInput("Left");
        rightStick = GetJoystickInput("Right");


        buttonA = Input.GetKeyDown(KeyCode.Joystick1Button0);
        buttons[0] = buttonA;

        joysticks[0] = leftStick.x;
        joysticks[1] = leftStick.y;
        joysticks[2] = rightStick.x;
        joysticks[3] = rightStick.y;

        Mouledoux.Callback.Packet inputData =
            new Mouledoux.Callback.Packet(new int[0], buttons, joysticks, new string[0]);

        Mouledoux.Components.Mediator.instance.NotifySubscribers("input", inputData);
    }

    private Vector2 GetJoystickInput(string joystick)
    {
        Vector2 stickPos = Vector2.zero;
        stickPos.x = Input.GetAxis("Horizontal" + joystick);
        stickPos.y = Input.GetAxis("Vertical" + joystick);
        return stickPos;
    }
}
