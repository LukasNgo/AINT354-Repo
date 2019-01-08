using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    [SerializeField] private Transform m_keyPrefab;

    private List<Transform> m_keySpawns = new List<Transform>();
    private List<Transform> m_chosenSpawns= new List<Transform>();

	void Start ()
    {
        // Get spawn locations
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            m_keySpawns.Add(this.gameObject.transform.GetChild(i));
        }

        //RandomlySelect(8);
        SelectAll();

        Spawn();
	}

    private void Spawn()
    {
        foreach (Transform spawn in m_chosenSpawns)
        {
            Instantiate(m_keyPrefab, spawn.position, spawn.rotation);
        }
    }

    private void RandomlySelect(int count)
    {
        int index;
        Transform chosenObject;

        for (int i = 0; i < count; i++)
        {
            // Randomly select spawn
            index = Random.Range(0, m_keySpawns.Count);
            chosenObject = m_keySpawns[index];

            // If spawn is already picked, keep chosing until an unpicked one is found
            while (chosenObject.tag == "Chosen")
            {
                index = Random.Range(0, m_keySpawns.Count);
                chosenObject = m_keySpawns[index];
            }

            chosenObject.tag = "Chosen";
            m_chosenSpawns.Add(chosenObject);
        }
    }

    private void SelectAll()
    {
        foreach (Transform child in m_keySpawns)
        {
            m_chosenSpawns.Add(child);
        }
    }
}
