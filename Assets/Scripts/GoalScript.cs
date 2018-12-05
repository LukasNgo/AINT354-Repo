using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private KeyInventory m_keyInventory;

    private bool m_hasCollided = false;
    private bool m_notEnoughKeys = false;
    private int m_isOpen = 0; // 0 Locked, 1 Unlocked (for "Door Unlocked!" prompt), 2 Uninteractable
    private Rigidbody m_door;

    private void Start()
    {
        //m_door = transform.Find("Door").GetComponent<Rigidbody>();
        //m_door.constraints = RigidbodyConstraints.FreezeRotationY;
        m_keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_hasCollided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_hasCollided = false;
        }
    }

    private void OnGUI()
    {
        string m_text;

        if (m_isOpen == 0 && m_hasCollided)
        {
            m_text = "Press E to open";
            GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 50), m_text);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        }

        if (m_isOpen == 0 && m_notEnoughKeys)            
        {
            if (m_keyInventory.GetKeyCount() == 1)
            {
                m_text = ("You have " + m_keyInventory.GetKeyCount() + " key out of 8");
            }
            else
            {
                m_text = ("You have " + m_keyInventory.GetKeyCount() + " keys out of 8");
            }
            
            GUI.Box(new Rect((Screen.width - 250) / 2, (Screen.height + 80) / 2, 250, 50), m_text);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        }

        if (m_isOpen == 1)
        {
            m_text = "Door unlocked!";
            GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 50), m_text);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;

            GameObject.FindObjectOfType<EndGame>().Win();
        }
    }

    private void Update()
    {
        if (m_hasCollided)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (m_keyInventory.GetKeyCount() >= 8)
                {
                    //m_door.constraints = RigidbodyConstraints.None;
                    m_keyInventory.RemoveKeys(5);
                    m_isOpen = 1;
                    Invoke("ToggleIsOpen", 2);
                }
                else
                {
                    m_notEnoughKeys = true;
                    Invoke("ToggleNotEnough", 2);
                }
            }
        }
    }

    private void ToggleNotEnough()
    {
        m_notEnoughKeys = false;
    }

    private void ToggleIsOpen()
    {
        m_isOpen = 2;
    }
}
