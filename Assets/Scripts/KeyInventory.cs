using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyInventory : MonoBehaviour
{
    private List<int> m_keyList = new List<int>();
    [SerializeField] private GameObject m_keyUI;

    private void Start()
    {
        m_keyUI.SetActive(false);
    }

    private void Update()
    {
        KeyGUI();        
    }

    public void AddKey()
    {
        m_keyList.Add(1);
        Debug.Log("list value = " + m_keyList.Count);
    }

    public void RemoveKeys(int value)
    {
        for (int i = 0; i < value; i++)
        {
            m_keyList.Remove(1);
        }
        
        Debug.Log("list value = " + m_keyList.Count);
    }

    public int GetKeyCount()
    {
        return m_keyList.Count;
    }

    public void KeyGUI()
    {

        if (m_keyList.Count > 0)
        {
            m_keyUI.SetActive(true);
        }
        else
        {
            m_keyUI.SetActive(false);
        }
        m_keyUI.GetComponentInChildren<Text>().text = ("Keys: " + m_keyList.Count);       
    }

}
