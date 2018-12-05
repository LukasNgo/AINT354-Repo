using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Animator m_camera;
    
    public void CamPosition2()
    {
        m_camera.SetBool("Animate", true);
    }

    public void CamPosition1()
    {
        m_camera.SetBool("Animate", false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

}
