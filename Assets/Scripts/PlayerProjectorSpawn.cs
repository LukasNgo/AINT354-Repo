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
            if (GetComponent<PlayerMovement>().isPlayerRunning())
            {
                if (echolocation.isEcholocationActive)
                {
                    objectPooler.SpawnFromPool("BLUE", transform.position, Quaternion.identity);
                }

                GameObject.FindGameObjectWithTag("Enemy").GetComponent<MonsterController>().SetNewDestination(GetComponent<Transform>());
            }
            yield return new WaitForSeconds(delay);
        }

    }

}
