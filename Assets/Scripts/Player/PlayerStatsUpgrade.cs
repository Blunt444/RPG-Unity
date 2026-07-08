using UnityEngine;

public class PlayerStatsUpgrade : MonoBehaviour
{

    private StatsManager statsManager;
    private PlayerHealth playerHealth;
    private StatsUI statsUI;

    private void Awake()
    {
        statsManager = StatsManager.Instance;
        playerHealth = GetComponent<PlayerHealth>();
        statsUI = GameObject.FindGameObjectWithTag("StatsPanel").GetComponent<StatsUI>();
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
        statsUI.UpdateAllStats();
    }

    public void UpdateGuardHitNegate(int amount)
    {
        statsManager.maxGuardHitNegate += amount;
    }

}


