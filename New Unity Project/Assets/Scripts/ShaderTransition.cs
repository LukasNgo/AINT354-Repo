using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTransition : MonoBehaviour
{
    public Material[] mats;

    [Range(0.0f, 1.0f)]
    public float transparency = 0f;

	void Start ()
    {
        mats = GetComponent<Renderer>().materials;
    }
	
	void Update ()
    {
        mats[1].SetFloat("_Transparency", transparency);
	}
}
