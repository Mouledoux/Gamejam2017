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
        transform.position = GetJoystickInput("Left");
    }

    private Vector2 GetJoystickInput(string joystick)
    {
        Vector2 stickPos = Vector2.zero;
        stickPos.x = Input.GetAxis("Horizontal" + joystick);
        stickPos.y = Input.GetAxis("Vertical" + joystick);
        return stickPos;
    }
}
