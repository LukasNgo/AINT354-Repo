using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private int m_health;
    private bool m_isAlive;
    private bool m_canRegen;
    private int m_maxHealth;
    [SerializeField]
    private GameObject PlayerDeathUI;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        m_health = 100;
        m_isAlive = true;
        m_canRegen = true;
        m_maxHealth = 100;
        PlayerDeathUI.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        m_health -= damage;

        if (m_health <= 0)
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
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0.04f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        PlayerDeathUI.SetActive(true);
        m_isAlive = false;
        Cursor.visible = true;
        transform.GetChild(0).GetComponent<SmoothCameraLook>().enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        PlayerDeathUI.SetActive(false);
        m_isAlive = true;
        Cursor.visible = false;
    }

    private IEnumerator RegenHealthRoutine()
    {
        float regenTimer = 3;

        yield return new WaitForSeconds(regenTimer);

        m_canRegen = true;
    }

    public void RegenHealth(int newRegenHealth)
    {
        m_health += newRegenHealth;
    }

    // getters & setters
    public float GetHealth()
    {
        return m_health;
    }

    public void SetHealth(int newHealth)
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

    public void SetMaxHealth(int newMaxHealth)
    {
        m_maxHealth = newMaxHealth;
    }
}
