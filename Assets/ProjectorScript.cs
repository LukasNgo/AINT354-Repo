using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScript : MonoBehaviour {

    [SerializeField]
    private Projector _projector;

    private float t = 0;

    [SerializeField]
    private int duration = 3;
    [SerializeField]
    private int destroyAfter = 5;

    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    void Update () {
        //transform.Rotate(Vector3.right * Time.deltaTime);

        t += Time.deltaTime / duration;
        _projector.fieldOfView = Mathf.Lerp(10, 180, t);
	}

}
