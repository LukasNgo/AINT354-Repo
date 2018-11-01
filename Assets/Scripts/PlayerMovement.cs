using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool m_isWalking = true;
    [SerializeField] private bool m_isRunning;
    [SerializeField] private bool m_isCreeping;
    [SerializeField] private float m_walkSpeed;
    [SerializeField] private float m_runSpeed;
    [SerializeField] private float m_creepSpeed;
    [SerializeField] private Camera m_camera;

    private Rigidbody m_rigidbody;
    private Vector3 m_inputs = Vector3.zero;

    void Start ()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_camera = GetComponent<Camera>();
	}
	
	void Update ()
    {
        m_inputs = Vector3.zero;
        m_inputs.x = Input.GetAxis("Horizontal");
        m_inputs.z = Input.GetAxis("Vertical");

        if (m_inputs != Vector3.zero) // Updates player steering
        {
            transform.forward = m_inputs;
        }

        //m_camera.LookRotation(transform, m_camera.transform);
    }

    void FixedUpdate()
    {
        float m_speed = m_walkSpeed;

        m_isRunning = Input.GetKey(KeyCode.LeftShift) && !m_isCreeping;
        m_isCreeping = Input.GetKey(KeyCode.LeftControl) && !m_isRunning;

        m_isWalking = !m_isRunning && !m_isCreeping;

        if (!m_isWalking && m_isRunning)
        {
            m_speed = m_runSpeed;
        }
        else if (!m_isWalking && m_isCreeping)
        {
            m_speed = m_creepSpeed;
        }
        else
        {
            m_speed = m_walkSpeed;
        }

        m_rigidbody.MovePosition(m_rigidbody.position + m_inputs * m_speed * Time.fixedDeltaTime);
    }
}
