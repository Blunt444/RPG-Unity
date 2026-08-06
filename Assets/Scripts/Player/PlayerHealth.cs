using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour, Damageable
{
    public TMP_Text healthText;
    public Animator healthTextAnim;

    [SerializeField]
    private Image fillImage;

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
        fillImage.fillAmount = (float)StatsManager.Instance.currentHealth / StatsManager.Instance.maxHealth;
    }

    public void TakeDamage(int damageAmount, Transform attacker)
    {
        //this inteface only serves one purpose which is
        //when multiple area/volume damage is dealth it is easier to use this.
        ChangeHealth(-damageAmount);
        
        Dynamite dynamite = attacker.GetComponent<Dynamite>();

        GetComponent<PlayerMovement>().Knockback(attacker, dynamite.knockbackForce, dynamite.knockbackTime);
    }
}
