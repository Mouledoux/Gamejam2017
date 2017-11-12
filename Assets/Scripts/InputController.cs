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

    #region Controller Inputs
    Vector2 leftStick = Vector2.zero;
    Vector2 rightStick = Vector2.zero;

    float leftTrigger = 0f;
    float rightTrigger = 0f;

    bool buttonA = false;
    bool buttonB = false;
    bool buttonX = false;
    bool buttonY = false;

    bool dPadUp = false;
    bool dPadDown = false;
    bool dPadLeft = false;
    bool dPadRight = false;

    bool leftBumber = false;
    bool rightBumber = false;

    #endregion

    bool[] buttons = new bool[16];
    float[] joysticks = new float[6];

    private void FixedUpdate()
    {
        GetControllerInput();
    }

    private void GetControllerInput()
    {
        leftStick = GetJoystickInput("Left");
        rightStick = GetJoystickInput("Right");

        leftTrigger = Mathf.Clamp(GetTriggerInput(), 0, 1);
        rightTrigger = Mathf.Abs(Mathf.Clamp(GetTriggerInput(), -1, 0));


        buttonA = Input.GetKeyDown(KeyCode.Joystick1Button0);

        

        joysticks[0] = leftStick.x;
        joysticks[1] = leftStick.y;
        joysticks[2] = rightStick.x;
        joysticks[3] = rightStick.y;
        joysticks[4] = leftTrigger;
        joysticks[5] = rightTrigger;


        buttons[0] = buttonA;


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

    private float GetTriggerInput()
    {
        return Input.GetAxis("Triggers");
    }
}
