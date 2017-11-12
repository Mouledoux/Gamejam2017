using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    private static float m_timeScale = 1f;
    public static float timeScale
    {
        get
        {
            return m_timeScale;
        }
        set
        {
            m_timeScale = value < 0 ? 0 : value;
        }
    }

    private static Vector3 m_globalGravity;
    public static Vector3 globalGravity
    {
        get
        {
            return m_globalGravity * timeScale;
        }
        set
        {
            m_globalGravity = value;
            m_globalGravity = m_globalGravity.magnitude > 10f ? (m_globalGravity.normalized * 10f) : m_globalGravity;
        }
    }

}
