using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float m_health;
    private bool m_isAlive;
    private bool m_canRegen;
    private float m_maxHealth;

    private void Awake()
    {
        m_health = 100;
        m_isAlive = true;
        m_canRegen = true;
        m_maxHealth = 100;
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;

        if (m_health == 0)
        {
            Death();
        }
        else
        {
            m_canRegen = false;
            StartCoroutine(RegenHealthRoutine());
        }
    }

    private void Death()
    {
        Destroy(this);

        m_isAlive = false;
    }

    private IEnumerator RegenHealthRoutine()
    {
        float regenTimer = 3;

        yield return new WaitForSeconds(regenTimer);

        m_canRegen = true;
    }

    public void RegenHealth(float newRegenHealth)
    {
        m_health += newRegenHealth;
    }

    // getters & setters
    public float GetHealth()
    {
        return m_health;
    }

    public void SetHealth(float newHealth)
    {
        m_health = newHealth;
    }

    public bool GetAlive()
    {
        return m_isAlive;
    }

    public void SetAlive(bool newAlive)
    {
        m_isAlive = newAlive;
    }

    public bool GetRegen()
    {
        return m_canRegen;
    }

    public void SetRegen(bool newRegen)
    {
        m_canRegen = newRegen;
    }

    public float GetMaxHealth()
    {
        return m_maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        m_maxHealth = newMaxHealth;
    }
}
