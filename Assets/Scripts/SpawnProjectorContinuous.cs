using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectorContinuous : MonoBehaviour {

    //public GameObject projector;

    [SerializeField]
    private float delay = 0.7f;
    [SerializeField]
    private EcholocationManager echolocation;

    private ObjectPooler objectPooler;

	void Start () {
        objectPooler = ObjectPooler.Instance;

        StartCoroutine(SpawnProjector());
	}
	
    private IEnumerator SpawnProjector()
    {
        while (true)
        {
            if (echolocation.isEcholocationActive)
            {
                objectPooler.SpawnFromPool("RED", transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(delay);
        }

    }
}
