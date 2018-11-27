using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour {
    // reference: https://stackoverflow.com/questions/39380901/shake-an-object-back-and-forth-with-easing-at-the-end

    private Vector3 m_defaultPos;
    private Quaternion m_defaultAng;

    private float m_radius = 10f;

    private void Start()
    {
        m_defaultPos = transform.position;
        m_defaultAng = Quaternion.identity;
        
    }
    private void Update()
    {
        float x = transform.position.x + Random.Range(-m_radius, m_radius);
        float z = transform.position.z + Random.Range(-m_radius, m_radius);
        float y = transform.position.y + Random.Range(-m_radius, m_radius);

        Vector3 newPos = new Vector3(x, y, z);
        transform.position = Vector3.Slerp(transform.position, newPos, Time.deltaTime);
        //transform.rotation = m_defaultAng * Quaternion.AngleAxis(Random.Range(-m_angleRot, m_angleRot), new Vector3(1f, 1f, 1f));
    }
}
