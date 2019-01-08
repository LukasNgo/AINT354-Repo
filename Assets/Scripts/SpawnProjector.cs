using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjector : MonoBehaviour {

    [SerializeField]
    private EcholocationManager echolocation;

    private ObjectPooler objectPooler;

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (echolocation.isEcholocationActive)
            {
                Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
                Vector3 pos = contact.point;
                objectPooler.SpawnFromPool("BLUE", pos, rot);
            }


            if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
            {
                FindObjectOfType<AudioManager>().PlayOneShot("Chair");
            }
        }

    }
}
