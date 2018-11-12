using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjector : MonoBehaviour {

    public GameObject projector;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(projector, pos, rot);
        }
    }
}
