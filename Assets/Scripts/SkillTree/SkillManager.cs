using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    private PlayerStatsUpgrade playerStatsUpgrade;

    private PlayerStatsUpgrade PlayerUpgrade =>
    playerStatsUpgrade ??= GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerStatsUpgrade>();

    private void Awake()
    {

        if (Instance == null)
        {
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
        if(PlayerUpgrade == null)
        {
            Debug.Log("Player Upgrade not yet init");
            return;
        }
        switch (effect.type)
        {
            case SkillEffectType.MaxHealth:
                PlayerUpgrade.UpdateMaxHealth(effect.amount);
                return;
            case SkillEffectType.GuardNegate:
                PlayerUpgrade.UpdateGuardHitNegate(effect.amount);
                return;
            case SkillEffectType.MoveSpeed:
                PlayerUpgrade.UpdateSpeed(effect.amount);
                return;
            case SkillEffectType.CombatAttackCooldown:
                PlayerUpgrade.UpdateAttackCooldown(-effect.amount, effect.type);
                return;
            case SkillEffectType.ArcheryAttackCooldown:
                PlayerUpgrade.UpdateAttackCooldown(-effect.amount, effect.type);
                return;
            case SkillEffectType.StunTime:
                PlayerUpgrade.UpdateStunTimer(effect.amount);
                return;
            case SkillEffectType.CombatDamage:
                PlayerUpgrade.UpdateDamage(effect.amount, effect.type);
                return;
            case SkillEffectType.ArrowCapacity:
                ArrowQuantityManager.Instance.IncreaseCapacity(effect.amount);
                return;
            case SkillEffectType.SpeedDamp:
                PlayerUpgrade.UpdateSpeedDamp(-effect.amount);
                return;
            case SkillEffectType.WeaponRange:
                PlayerUpgrade.UpdateWeaponRange(effect.amount);
                return;
            case SkillEffectType.ArcherDamageDeflect:
                PlayerUpgrade.UpdateArcherDamageDeflect(effect.amount);
                return;
            default:
                return;
        }
    }

}
