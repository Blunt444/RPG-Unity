using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Animator healthTextAnim;

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
            gameObject.SetActive(false);
        }

        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthText.text = "HP: " + StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
    }
}
