using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementRevised : MonoBehaviour {
    // reference: https://medium.com/ironequal/unity-character-controller-vs-rigidbody-a1e243591483

    private Rigidbody m_rb;

    [SerializeField]
    private float m_speed = 50f;

    [SerializeField]
    private float m_sprintSpeed = 100f;

    [SerializeField]
    private float m_creepSpeed = 10f;

    private bool m_sprint = false;
    private bool m_creep = false;

    private float horizontal;
    private float vertical;
    

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        m_sprint = Input.GetKey(KeyCode.LeftShift);
        m_creep = Input.GetKey(KeyCode.Z);

       
    }

    private void FixedUpdate()
    {
        if (!m_sprint && !m_creep)
        {
            MovePlayer(m_speed);
            Debug.Log("Normal");
        }
        else if (m_sprint && !m_creep)
        {
            MovePlayer(m_sprintSpeed);
            Debug.Log("Sprint");
        }
        else if (!m_sprint && m_creep)
        {
            MovePlayer(m_creepSpeed);
            Debug.Log("Creep");
        }
        
    }

    public void MovePlayer(float _speed)
    {
        m_rb.MovePosition(m_rb.position + (transform.forward * vertical) * _speed * Time.fixedDeltaTime);
        m_rb.MovePosition(m_rb.position + (transform.right * horizontal) * _speed * Time.fixedDeltaTime);
    }
}

