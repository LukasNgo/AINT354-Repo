using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightsScript : MonoBehaviour {

    private float m_maxIntensity;
    private Light m_light;

    private LightsController m_lightController_script;

	// Use this for initialization
	void Start () {
        m_light = GetComponent<Light>();
        m_maxIntensity = m_light.intensity;
        m_lightController_script = GameObject.Find("LightsManager").GetComponent<LightsController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (m_lightController_script.getLightsOn() == false)
        {
            m_light.intensity = Mathf.Lerp(m_light.intensity, 0, Time.deltaTime * 2);
            //Debug.Log(m_light.intensity);
            if (m_light.intensity < 0.005)
            {
                m_light.intensity = 0;
            }
        }
        else if (m_lightController_script.getLightsOn() == true)
        {
            m_light.intensity = Mathf.Lerp(m_light.intensity, m_maxIntensity, Time.deltaTime / 2);
            //Debug.Log(m_light.intensity);
            if (m_light.intensity > m_maxIntensity - 0.005)
            {
                m_light.intensity = m_maxIntensity;
            }
        }
	}
}
