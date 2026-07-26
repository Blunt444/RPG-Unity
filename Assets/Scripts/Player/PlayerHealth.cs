using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;

    public static event Action OnPlayerDeath;

    private void Start()
    {
        UpdateHealthUI();
    }
    public void ChangeHealth(int amount)
    {
        StatsManager.Instance.currentHealth += amount;

        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }
        else
        {
            healthTextAnim.Play("TextUpdate");
        }

        if (StatsManager.Instance.currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthUI();
    }

    public void Die()
    {
        OnPlayerDeath?.Invoke();
        gameObject.SetActive(false);
    }

    public void UpdateHealthUI()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
}
