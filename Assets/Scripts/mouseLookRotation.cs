using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLookRotation : MonoBehaviour {

    // reference: https://forum.unity.com/threads/looking-with-the-mouse.109250/

    private float m_sensitivityX = 15f;
    private float m_sensitivityY = 15f;

    private float m_rotationY = 0f;
    private float m_rotationX = 0f;

    [SerializeField]
    private Camera m_camera;

    private Rigidbody m_rb;

    private void Start()
    {
        m_camera = GameObject.Find("Camera").GetComponent<Camera>();
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        m_rotationX += Input.GetAxis("Mouse X") * m_sensitivityX;
        m_rotationY += Input.GetAxis("Mouse Y") * m_sensitivityY;

        m_rotationY = Mathf.Clamp(m_rotationY, -60f, 60f);

        m_camera.transform.rotation = Quaternion.Euler(-m_rotationY, m_rotationX, 0f);
    }

    private void FixedUpdate()
    {
        m_rb.transform.rotation = Quaternion.Euler(0f, m_rotationX, 0f);
    }
}
