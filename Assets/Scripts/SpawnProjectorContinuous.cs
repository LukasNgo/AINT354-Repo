using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectorContinuous : MonoBehaviour {

    public GameObject projector;
    //public GameObject projector2;
    [SerializeField]
    private float delay = 0.7f;
    [SerializeField]
    private EcholocationManager echolocation;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnProjector());
	}
	


    private IEnumerator SpawnProjector()
    {
        while (true)
        {
            if (echolocation.isEcholocationActive)
            {
                Instantiate(projector, transform.position, transform.rotation);
                //Instantiate(projector2, transform.position, transform.rotation);
            }
            yield return new WaitForSeconds(delay);
        }

    }
}
