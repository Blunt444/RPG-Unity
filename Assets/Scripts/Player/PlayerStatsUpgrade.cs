using UnityEngine;

public class PlayerStatsUpgrade : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void UpdateMaxHealth(int amount)
    {
        StatsManager.Instance.maxHealth += amount;
        playerHealth.UpdateHealthUI();
    }

    public void UpdateHealth(int amount)
    {
        playerHealth.ChangeHealth(amount);
    }

    public void UpdateStunTimer(int amount)
    {
        StatsManager.Instance.stunTime += StatsManager.Instance.baseStunTime * (amount / 100.0f);
    }

    public void UpdateSpeedDamp(int amount)
    {
        StatsManager.Instance.speedDamp -= StatsManager.Instance.baseSpeedDamp * (amount / 100.0f);
    }

    public void UpdateDamage(int amount, SkillEffectType type)
    {
        if (type == SkillEffectType.CombatDamage)
            StatsManager.Instance.damage += amount;
        else if (type == SkillEffectType.ArrowDamage)
            StatsManager.Instance.arrowDamage += amount;
    }

    public void UpdateAttackCooldown(int amount, SkillEffectType type)
    {
        if (type == SkillEffectType.CombatAttackCooldown)
            StatsManager.Instance.combatAttackCooldown -= StatsManager.Instance.baseCombatAttackCooldown * (amount / 100.0f);
        else if (type == SkillEffectType.ArcheryAttackCooldown)
            StatsManager.Instance.archeryAttackCooldown -= StatsManager.Instance.baseArcheryAttackCooldown * (amount / 100.0f);
    }

    public void UpdateWeaponRange(int amount)
    {
        StatsManager.Instance.weaponRange += StatsManager.Instance.baseWeaponRange * (amount / 100.0f);
    }

    public void UpdateSpeed(int amount)
    {
        StatsManager.Instance.speed += StatsManager.Instance.baseSpeed * (amount / 100.0f);
        StatsUI.Instance.UpdateAllStats();
    }

    public void UpdateArcherDamageDeflect(int amount)
    {
        StatsManager.Instance.archerDamageDeflect += StatsManager.Instance.baseArcherDamageDeflect * (amount / 100.0f);
    }

    public void UpdateGuardHitNegate(int amount)
    {
        StatsManager.Instance.maxGuardHitNegate += amount;
    }
}