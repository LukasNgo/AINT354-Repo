using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseManager : MonoBehaviour
{
    public GameObject m_pauseCanvas;
    public AudioMixer m_audioMixer;
    [SerializeField] private Slider m_volumeSlider;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (m_pauseCanvas.activeInHierarchy == false)
        {
            m_pauseCanvas.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
        else
        {
            m_pauseCanvas.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }

    public void ResumeGame()
    {
        m_pauseCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ToggleCursor()
    {
        Cursor.lockState = (Cursor.lockState == CursorLockMode.None) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = (Cursor.visible == false) ? true : false;
    }

    // Set fullscreen according to toggle value
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Set master mixer volume to increments of 5 using options slider
    public void SetVolume(float volume)
    {
        float value = m_volumeSlider.value;
        float interval = 5.0f;
        value = Mathf.Round(value / interval) * interval;

        m_volumeSlider.value = value;
        m_audioMixer.SetFloat("volume", value);
    }
}
