using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScript : MonoBehaviour {

    [SerializeField]
    private Projector _projector;
    [SerializeField]
    private int min = 10;
    [SerializeField]
    private int max = 180;

    private float t = 0;

    [SerializeField]
    private int duration = 3;

    private void Start()
    {
        Destroy(gameObject, duration);
    }

    void Update () {
        transform.Rotate(Vector3.right * Time.deltaTime);

        t += Time.deltaTime / duration;
        _projector.fieldOfView = Mathf.Lerp(min, max, t);
	}

}
