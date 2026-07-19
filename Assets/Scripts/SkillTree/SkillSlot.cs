using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[Serializable]
public class ReslovedPrerequisiteSkillSlots
{
    public SkillSlot slot;
    public int requiredLevel;
}

public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillSO skillSO;
    public Image skillIcon;
    public TMP_Text skillLvlText;
    public Button skillButton;
    public int currentLevel = 0;
    public bool isUnlocked;

    [NonSerialized] public List<ReslovedPrerequisiteSkillSlots> prerequisiteSkillSlots = new List<ReslovedPrerequisiteSkillSlots>();

    public void Setup(SkillSO skillSO)
    {
        this.skillSO = skillSO;
        UpdateUI();
    }

    private void Awake()
    {
        skillButton.onClick.AddListener(() => Debug.Log("RAW CLICK on: " + gameObject.name));
    }

    public void AddOnClickToUpgrade()
    {
        Debug.Log("Listener added for: " + (skillSO != null ? skillSO.skillName : "NULL skillSO"));
        skillButton.onClick.AddListener(() => SkillTreeManager.Instance.TryUpgradeSkill(this));
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
            UpdateUI();
            SkillInfo.Instance.SetCostText(ReturnCurrentSkillCost());
            return true;
        }

        return false;
    }

    public int ReturnCurrentSkillCost()
    {
        return skillSO.initialCost + ((currentLevel) <= 0 ? 0 : currentLevel - 1) * skillSO.incrementValue;
    }

    private void UpdateUI()
    {

        if (skillSO == null) return;

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
            skillLvlText.text = "Lock";
            skillIcon.color = Color.grey;
        }
    }

    public void ShowPrerequestOnHoverIfNotUnlocked()
    {
        if (!isUnlocked)
            SkillInfo.Instance.ShowPrerequestOnHover(prerequisiteSkillSlots);
        SkillInfo.Instance.SetCostYPos(prerequisiteSkillSlots.Count == 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillInfo.Instance.SetPanelPosition(eventData.position);
        SkillInfo.Instance.SetPanelState(true);
        SkillInfo.Instance.SetCostText(ReturnCurrentSkillCost());
        ShowPrerequestOnHoverIfNotUnlocked();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillInfo.Instance.SetPanelState(false);
        SkillInfo.Instance.ClearPrerequestOnExitHover();
    }
}
