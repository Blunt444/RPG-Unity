using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;
    public SkillCategory category;
    public List<SkillSO> prerequisites;
}
public enum SkillCategory { Combat, Magic };