using UnityEngine;

public class PlayerStatsUpgrade : MonoBehaviour
{

    private StatsManager statsManager;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        statsManager = StatsManager.Instance;
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void UpdateMaxHealth(int amount)
    {
        statsManager.maxHealth += amount;
        playerHealth.UpdateHealthUI();
    }
    public void UpdateHealth(int amount)
    {
        playerHealth.ChangeHealth(amount);
    }

    public void UpdateSpeed(int amount)
    {
        statsManager.speed += amount;
        StatsUI.Instance.UpdateAllStats();
    }

    public void UpdateGuardHitNegate(int amount)
    {
        statsManager.maxGuardHitNegate += amount;
    }

}


