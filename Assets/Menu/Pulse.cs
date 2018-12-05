using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour {


    private bool pulseOn = false;

    // Use this for initialization
    void Start () {
        InvokeRepeating("pulse", 1f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (pulseOn)
        {
            Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 1, Time.deltaTime * 4)));
        }
        else
        {
            Shader.SetGlobalFloat("_TransparencyMaterial", (Mathf.Lerp(Shader.GetGlobalFloat("_TransparencyMaterial"), 0, Time.deltaTime * 2)));
        }
    }

        private void pulse()
        {
            if (pulseOn)
            {
                pulseOn = false;
            }
            else
            {
                pulseOn = true;
            }
        }
    
}
