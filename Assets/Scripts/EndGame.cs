using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour {

    [SerializeField]
    private GameObject EscapeText;

    // Use this for initialization
    void Start () {
        EscapeText.SetActive(false);
    }

    public void Win()
    {
        EscapeText.SetActive(true);
    }
}
