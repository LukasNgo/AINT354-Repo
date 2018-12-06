using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTorch : MonoBehaviour {

    private Light m_light;
    private bool m_cooldown;

    private void Start()
    {
        m_light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(Toggle());
        }
	}

    IEnumerator Toggle()
    {
        if (!m_cooldown)
        {
            m_cooldown = true;

            //Debug.Log("torch state: " + m_light.enabled);
            if (m_light.enabled == true)
            {
                m_light.enabled = false;
            }
            else
            {
                m_light.enabled = true;
            }

            yield return new WaitForSeconds(0.1f);

            m_cooldown = false;
        }
    }
}
