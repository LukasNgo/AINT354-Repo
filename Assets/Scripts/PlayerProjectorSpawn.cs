using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectorSpawn : MonoBehaviour {

    [SerializeField]
    private float delay = 0.3f;
    [SerializeField]
    private EcholocationManager echolocation;

    private ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;

        StartCoroutine(SpawnProjector());
    }

    private IEnumerator SpawnProjector()
    {
        while (true)
        {
            if (echolocation.isEcholocationActive && GetComponent<PlayerMovement>().isPlayerRunning())
            {
                objectPooler.SpawnFromPool("BLUE", transform.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(delay);
        }

    }

}
