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
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.04f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Cursor.visible = true;
        GameObject.FindObjectOfType<SmoothCameraLook>().enabled = false;
    }
}
