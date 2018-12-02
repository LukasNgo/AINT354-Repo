using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTorch : MonoBehaviour {

    private bool m_torchOn = true;
    private Light m_light;

    private void Start()
    {
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_torchOn && m_light.enabled == true)
            {
                m_light.enabled = false;
                m_torchOn = false;
            }
            else if (!m_torchOn && m_light.enabled == false)
            {
                m_light.enabled = true;
                m_torchOn = true;
            }
        }
	}
}
