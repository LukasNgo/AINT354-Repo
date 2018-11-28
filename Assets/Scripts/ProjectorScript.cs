using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorScript : MonoBehaviour{

    [SerializeField]
    private Projector _projector;
    [SerializeField]
    private int min = 10;
    [SerializeField]
    private int max = 180;
    [SerializeField]
    private float duration = 3;

    private void OnEnable()
    {
        StartCoroutine(PulseEffect());
    }

    private IEnumerator PulseEffect()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.Rotate(Vector3.right * Time.deltaTime);
            elapsedTime += Time.deltaTime / duration;
            _projector.fieldOfView = Mathf.Lerp(min, max, elapsedTime);
            yield return null;
        }
        
        gameObject.SetActive(false);
    }
}
