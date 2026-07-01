using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;
    public Image skillIcon;
    public TMP_Text skillLvlText;
    public Button skillButton;
    public int currentLevel;
    public bool isUnlocked;

    [NonSerialized] public List<SkillSlot> prerequisiteSkillSlots = new List<SkillSlot>();
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
            if (currentLevel >= skillSO.maxLevel)
            {
                OnSkillMaxed?.Invoke(this);
            }
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
        foreach(SkillSlot slot in prerequisiteSkillSlots)
        {
            if(!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
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
