using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyInventory m_keyInventory;

    private bool m_hasCollided = false;
    private string m_labelText;

    private void Start()
    {
        m_keyInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_hasCollided = true;
            m_labelText = "Press E to pickup key";            
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
        if (m_hasCollided == true)
        {
            GUI.Box(new Rect((Screen.width - 100) / 2, (Screen.height - 50) / 2, 250, 50), m_labelText);
            GUI.skin.box.fontSize = 20;
            GUI.skin.box.alignment = TextAnchor.MiddleCenter;
        }
    }

    private void Update()
    {
        if (m_hasCollided == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_keyInventory.AddKey();
                FindObjectOfType<AudioManager>().Play("Key");
                Destroy(this);
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
