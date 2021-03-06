﻿using System.Collections;
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

    private Vector3 m_inputs = Vector3.zero;
	
	void Update ()
    {
        m_inputs = Vector3.zero;
        m_inputs.x = Input.GetAxis("Horizontal");
        m_inputs.z = Input.GetAxis("Vertical");
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

            m_inputs.x *= Time.deltaTime;
            m_inputs.z *= Time.deltaTime;

            transform.Translate(m_inputs.x * m_speed, 0, m_inputs.z * m_speed);
    }   

    public bool isPlayerRunning()
    {
        return m_isRunning;
    }
}
