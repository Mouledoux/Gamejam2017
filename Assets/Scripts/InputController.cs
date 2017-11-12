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

    private Vector2 GetLeftJoystickInput()
    {
        Vector2 stickPos = Vector2.zero;
        stickPos.x = Input.GetAxis("Horizontal");
        stickPos.y = Input.GetAxis("Vertical");
        return stickPos;
    }

    private Vector2 GetRightJoystickInput()
    {
        Vector2 stickPos = Vector2.zero;
        stickPos.x = Input.GetAxis("Horizontal");
        stickPos.y = Input.GetAxis("Vertical");
        return stickPos;
    }
}
