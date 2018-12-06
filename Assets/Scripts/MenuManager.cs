using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator m_camera;
    public AudioMixer m_audioMixer;

    [SerializeField] private Slider m_volumeSlider;
    [SerializeField] private Dropdown m_resolutionDropdown;

    private Resolution[] m_resolutions;

    // Find all resolutions and populate the Resolution Dropdown with the values cast as strings
    void Start()
    {
        m_resolutions = Screen.resolutions;

        m_resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;

        for (int i = 0; i < m_resolutions.Length; i++)
        {
            string option = m_resolutions[i].width + " x " + m_resolutions[i].height + " " + m_resolutions[i].refreshRate + "Hz";
            options.Add(option);

            if (m_resolutions[i].width == Screen.currentResolution.width &&
                    m_resolutions[i].height == Screen.currentResolution.height &&
                        m_resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResIndex = i;
            }
        }

        m_resolutionDropdown.AddOptions(options);
        m_resolutionDropdown.value = currentResIndex;
        m_resolutionDropdown.RefreshShownValue();
    }

    public void CamPosition1()
    {
        m_camera.SetBool("Animate", false);
    }

    public void CamPosition2()
    {
        m_camera.SetBool("Animate", true);
    }    

    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitApplication()
    {
        Application.Quit();
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

    // Set quality acording to dropdown index
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    // Set fullscreen according to toggle value
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Set resolution according to dropdown index
    public void SetResolution(int resIndex)
    {
        Resolution resolution = m_resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
