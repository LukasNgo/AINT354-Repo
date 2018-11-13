using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTransition : MonoBehaviour
{
    // Get materials to manipulate from serialized variables
    [SerializeField] private Material defaultShader;
    [SerializeField] private Material blindShader;

    [Range(0.0f, 1.0f)]
    public float transparency = 0f;

	void Start ()
    {
        Material[] mats = new Material[2];
        mats[0] = new Material(defaultShader);
        mats[1] = new Material(blindShader);

        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).GetComponent<Renderer>().materials = mats;
        }
    }
	
	void Update ()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Material[] childMats = this.transform.GetChild(i).GetComponent<Renderer>().materials;

            childMats[1].SetFloat("_Transparency", transparency);
        }
    }

}
