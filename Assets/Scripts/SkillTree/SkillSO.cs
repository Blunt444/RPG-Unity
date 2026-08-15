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
    public List<SkillEffect> skillEffects = new List<SkillEffect>();
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
public class SkillEffect
{
    public Sprite icon;
    public float amount;
}