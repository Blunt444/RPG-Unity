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

    public void UpdateStunTimer(int amount)
    {
        statsManager.stunTime += statsManager.baseStunTime * (amount / 100.0f);
    }

    public void UpdateSpeedDamp(int amount)
    {
        statsManager.speedDamp -= statsManager.baseSpeedDamp * (amount / 100.0f);
    }

    public void UpdateDamage(int amount)
    {
        statsManager.damage += amount;
    }

    public void UpdateAttackCooldown(int amount, SkillEffectType type)
    {
        if (type == SkillEffectType.CombatAttackCooldown)
            statsManager.combatAttackCooldown -= statsManager.baseCombatAttackCooldown * (amount / 100.0f);
        else if (type == SkillEffectType.ArcheryAttackCooldown)
            statsManager.archeryAttackCooldown -= statsManager.baseArcheryAttackCooldown * (amount / 100.0f);
    }

    public void UpdateWeaponRange(int amount)
    {
        statsManager.weaponRange += statsManager.baseWeaponRange * (amount / 100.0f);
    }

    public void UpdateSpeed(int amount)
    {
        statsManager.speed += statsManager.baseSpeed * (amount / 100.0f);
        StatsUI.Instance.UpdateAllStats();
    }

    public void UpdateGuardHitNegate(int amount)
    {
        statsManager.maxGuardHitNegate += amount;
    }

}


