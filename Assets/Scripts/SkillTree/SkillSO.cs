using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;
    public SkillCategory category;
    public List<SkillPrerequisite> prerequisites;
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