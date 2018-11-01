using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenPlayerHealth : MonoBehaviour {

    private Player m_player;
    private float regenTime = 5;
    private float repeatRate = 1;

    private void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InvokeRepeating("Regenerate", regenTime, repeatRate);
    }

    private void Regenerate()
    {
        if (m_player.GetRegen() == true && m_player.GetHealth() < m_player.GetMaxHealth())
        {
            m_player.RegenHealth(1);
        }
    }
}
