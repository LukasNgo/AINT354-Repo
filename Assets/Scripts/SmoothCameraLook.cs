using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraLook : MonoBehaviour
{
    private Vector2 m_mouselook;
    private Vector2 m_smoothV;
    [SerializeField] private float m_sensitivity = 2.0f;
    [SerializeField] private float m_smoothing = 1.0f;

    private GameObject m_player;

	void Start ()
    {
        m_player = this.transform.parent.gameObject;
	}
	
	void Update ()
    {
        if (Cursor.visible == false)
        {
            Vector2 deltaMouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

            deltaMouse = Vector2.Scale(deltaMouse, new Vector2(m_sensitivity * m_smoothing, m_sensitivity * m_smoothing));
            m_smoothV.x = Mathf.Lerp(m_smoothV.x, deltaMouse.x, 1f / m_smoothing);
            m_smoothV.y = Mathf.Lerp(m_smoothV.y, deltaMouse.y, 1f / m_smoothing);
            m_mouselook += m_smoothV;

            m_mouselook.y = Mathf.Clamp(m_mouselook.y, -90f, 90f); // Lock vertical rotation to 180°

            transform.localRotation = Quaternion.AngleAxis(-m_mouselook.y, Vector3.right);
            m_player.transform.localRotation = Quaternion.AngleAxis(m_mouselook.x, m_player.transform.up);
        }        
	}
}
