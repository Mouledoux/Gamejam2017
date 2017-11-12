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
        bool[] buttons = new bool[16];
        float[] joysticks = new float[6];

        Vector2 leftStick = GetJoystickInput("Left");
        Vector2 rightStick = GetJoystickInput("Right");

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
