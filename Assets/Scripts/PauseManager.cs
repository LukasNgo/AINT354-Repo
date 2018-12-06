using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject m_pauseCanvas;

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
}
