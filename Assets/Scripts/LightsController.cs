using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour {

    private bool isLightOn = true;

    private Light[] Lights;
    void Start()
    {
        Lights = Object.FindObjectsOfType<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLightOn = !isLightOn;
        }

        if (isLightOn)
        {
            foreach (Light x in Lights)
            {
                x.enabled = true;
            }
        }
        else
        {
            foreach (Light x in Lights)
            {
                x.enabled = false;
            }
        }
    }
}
