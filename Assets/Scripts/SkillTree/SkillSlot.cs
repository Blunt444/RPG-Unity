using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

[Serializable]
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
    public Image whiteBg;
    public Image blackBg;

    [NonSerialized] public List<ReslovedPrerequisiteSkillSlots> prerequisiteSkillSlots = new List<ReslovedPrerequisiteSkillSlots>();

    public void Setup(SkillSO skillSO)
    {
        this.skillSO = skillSO;
        UpdateUI();
    }
    
    private void OnValidate()
    {
        UpdateUI();
    }

    public bool CanUnlockSkill()
    {
        foreach (ReslovedPrerequisiteSkillSlots rpss in prerequisiteSkillSlots)
        {
            if (!rpss.slot.isUnlocked || rpss.slot.currentLevel < rpss.requiredLevel)
            {
                return false;
            }
        }

        return true;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
        UpdateUI();
    }

    public bool UpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSO.maxLevel && ReturnCurrentSkillCost() <= SkillTreeManager.Instance.GetCurrentPoints())
        {
            currentLevel++;
            SkillManager.Instance.HandleSkillUpgrade(this);
            SkillTreeManager.Instance.DeductPoints(ReturnCurrentSkillCost());
        }

        return false;
    }

    public int ReturnCurrentSkillCost()
    {
        return skillSO.initialCost + (currentLevel - 1) * skillSO.incrementValue;
    }

    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillLvlText.text = currentLevel.ToString() + "/" + skillSO.maxLevel.ToString();
            skillIcon.color = Color.white;
            whiteBg.gameObject.SetActive(true);
            blackBg.gameObject.SetActive(true);
        }
        else
        {
            skillButton.interactable = false;
            skillIcon.color = Color.grey;
            whiteBg.gameObject.SetActive(false);
            blackBg.gameObject.SetActive(false);
        }
    }
}
