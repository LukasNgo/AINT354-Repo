using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour {

    [SerializeField]
    private EcholocationManager echolocation;

    private Light[] Lights;
    void Start()
    {
        Lights = Object.FindObjectsOfType<Light>();
    }

    void Update()
    {
        if (echolocation.isEcholocationActive)
        {
            foreach (Light x in Lights)
            {
                x.enabled = false;
            }
        }
        else
        {
            foreach (Light x in Lights)
            {
                x.enabled = true;
            }
        }
    }
}
