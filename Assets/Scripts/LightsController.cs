using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour {

    [SerializeField]
    private EcholocationManager echolocation;

    //private bool getOriginal = false;

    private Light[] Lights;

    //private Dictionary<Light, float> lightWithIntensities;

    private bool lightsOn = false;

    private void Awake()
    {
        Lights = Object.FindObjectsOfType<Light>();

        foreach (Light x in Lights)
        {
            x.gameObject.AddComponent<lightsScript>();
        }
    }

    void Start()
    {
        //lightWithIntensities = new Dictionary<Light, float>();

        //foreach(Light x in Lights)
        //{
        //    lightWithIntensities.Add(x, x.intensity);
        //    Debug.Log(x + ": " + x.intensity);
        //}
    }

    void Update()
    {
        if (echolocation.isEcholocationActive)
        {
            //// turn lights off
            //foreach (KeyValuePair<Light, float> keyValue in lightWithIntensities)
            //{
            //    Light light = keyValue.Key;
            //    float intensity = keyValue.Value;

            //    // changing light intensity to lerp from original intensity
            //    light.intensity = Mathf.Lerp(intensity, 0, Time.deltaTime);
            //    Debug.Log("Light intensity" + light.intensity);
            //}
            lightsOn = false;
        }
        else
        {
            // turn lights on'
            lightsOn = true;
        }
    }

    public bool getLightsOn()
    {
        return lightsOn;
    }
}
