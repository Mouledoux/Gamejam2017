using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGravityGauge : MonoBehaviour
{
    public float m_radius;
    private RectTransform m_transform;

	// Use this for initialization
	void Start ()
    {
        m_transform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_transform.localPosition =
            (Utilities.globalGravity.normalized) *
            ((Utilities.globalGravity.magnitude / 10f) * m_radius);
	}
}