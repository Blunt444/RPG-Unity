using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    private PlayerStatsUpgrade playerStatsUpgrade;

    private void Awake()
    {

        if (Instance == null)
        {
            playerStatsUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsUpgrade>();
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleSkillUpgrade(SkillSlot slot)
    {
        string name = slot.skillSO.skillName;

        foreach (SkillEffect effect in slot.skillSO.skillEffects[slot.currentLevel - 1].effects)
        {
            ApplyEffect(effect);
        }
    }

    private void ApplyEffect(SkillEffect effect)
    {
        switch (effect.type)
        {
            case SkillEffectType.MaxHealth:
                playerStatsUpgrade.UpdateMaxHealth(effect.amount);
                return;
            case SkillEffectType.GuardNegate:
                playerStatsUpgrade.UpdateGuardHitNegate(effect.amount);
                return;
            case SkillEffectType.MoveSpeed:
                playerStatsUpgrade.UpdateSpeed(effect.amount);
                return;
            case SkillEffectType.CombatAttackCooldown:
                playerStatsUpgrade.UpdateAttackCooldown(effect.amount, effect.type);
                return;
            case SkillEffectType.ArcheryAttackCooldown:
                playerStatsUpgrade.UpdateAttackCooldown(effect.amount, effect.type);
                return;
            case SkillEffectType.StunTime:
                playerStatsUpgrade.UpdateStunTimer(effect.amount);
                return;
            case SkillEffectType.Damage:
                playerStatsUpgrade.UpdateDamage(effect.amount);
                return;
            case SkillEffectType.ArrowCapacity:
                ArrowQuantityManager.Instance.IncreaseCapacity(effect.amount);
                return;
            case SkillEffectType.SpeedDamp:
                playerStatsUpgrade.UpdateSpeedDamp(effect.amount);
                return;
            default:
                return;
        }
    }
}
