using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    private List<int> m_keyList = new List<int>();

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
}
