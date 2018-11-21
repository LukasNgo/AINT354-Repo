using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private KeyInventory m_keyInventory;

    private bool m_hasCollided = false;
    private bool notEnoughKeys = false;
    private string m_labelText;
    private Rigidbody m_door;
    private void Start()
    {
        m_door = transform.Find("Door").GetComponent<Rigidbody>();
        m_door.constraints = RigidbodyConstraints.FreezeRotationY;
        m_keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered");
        if (other.tag == "Player")
        {
            m_hasCollided = true;
            m_labelText = "Press E to open";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (other.tag == "Player")
        {
            m_hasCollided = false;
        }
    }

    private void OnGUI()
    {
        if (m_hasCollided)
        {
            GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 50), m_labelText);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        }

        if (notEnoughKeys)
        {
            string m_text = ("You have " + m_keyInventory.GetKeyCount() + " keys out of 5");
            GUI.Box(new Rect((Screen.width - 250) / 2, (Screen.height + 80) / 2, 250, 50), m_text);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        }
    }

    private void Update()
    {
        if (m_hasCollided)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_keyInventory.GetKeyCount() >= 5)
                {
                    m_door.constraints = RigidbodyConstraints.None;
                    m_keyInventory.RemoveKeys(5);
                }
                else
                {
                    notEnoughKeys = true;
                    Invoke("ToggleNotEnough", 2);
                }
            }
        }
    }

    private void ToggleNotEnough()
    {
        notEnoughKeys = !notEnoughKeys;
    }
}
