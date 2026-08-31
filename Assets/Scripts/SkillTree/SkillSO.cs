using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;
    public SkillCategory category;
    public List<SkillPrerequisite> prerequisites;
    public List<SkillLevelData> skillEffects = new List<SkillLevelData>();
    public int initialCost;
    public int incrementValue;
}
public enum SkillCategory { Combat, Archery };

[System.Serializable]
public class SkillPrerequisite
{
    public SkillSO skillSO;
    public int requiredLevel = 1;
}
[System.Serializable]
public enum SkillEffectType
{
    MaxHealth,
    MoveSpeed,
    CombatDamage,
    ArrowDamage,
    ArrowCapacity,
    GuardNegate,
    CombatAttackCooldown,
    ArcheryAttackCooldown,
    WeaponRange,
    StunTime,
    SpeedDamp,
    ArcherDamageDeflect,
}

[System.Serializable]
public class SkillLevelData
{
    public List<SkillEffect> effects = new List<SkillEffect>();
}

[System.Serializable]
public class SkillEffect
{
    public SkillEffectType type;
    public Sprite icon;
    public int amount;
    public bool isPercentage = false;
}