using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

[System.Serializable]
public class ReslovedPrerequisiteSkillSlots
{
    public SkillSlot slot;
    public int requiredLevel;
}

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public Image skillIcon;
    public TMP_Text skillLvlText;
    public Button skillButton;
    public int currentLevel;
    public bool isUnlocked;

    [NonSerialized] public List<ReslovedPrerequisiteSkillSlots> prerequisiteSkillSlots = new List<ReslovedPrerequisiteSkillSlots>();
    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    public void Setup(SkillSO skillSO)
    {
        this.skillSO = skillSO;
        UpdateUI();
    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel)
        {
            currentLevel++;
            OnAbilityPointSpent?.Invoke(this);
            UpdateUI();
        }
    }
    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }
    public bool CanUnlockSkill()
    {
        foreach(ReslovedPrerequisiteSkillSlots prereq in prerequisiteSkillSlots)
        {
            if(!prereq.slot.isUnlocked || prereq.slot.currentLevel < prereq.requiredLevel)
            {
                return false;
            }
        }

        return true;
    }
    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLvlText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;
            skillLvlText.text = "Locked";
            skillIcon.color = Color.grey;
        }

    }

}
