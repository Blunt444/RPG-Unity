using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{

    public static SkillTreeManager Instance;

    public List<SkillSO> allSkillSOs;
    public SkillSlot skillSlotPrefab;
    public Transform combatPanel;
    public Transform archeryPanel;
    public TMP_Text pointsText;
    public TMP_Text title;
    SkillCategory currentType = SkillCategory.Combat;

    [NonSerialized] private Dictionary<string, SkillSlot> skillSlotDictionary = new Dictionary<string, SkillSlot>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            ReadyAllSkills();
            UpdateUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Dictionary<string, SkillSlot> GetAllSkills()
    {
        return skillSlotDictionary;
    }

    public void SetSKillData(SkillData skillData)
    {
        if(skillSlotDictionary.TryGetValue(skillData.skillName, out SkillSlot slot))
        {
            slot.currentLevel = skillData.lvl;
            slot.isUnlocked = skillData.isUnlocked;
            slot.UpdateUI();
        }
    }

    public int GetCurrentPoints()
    {
        return StanceManager.Instance.GetPointsForRespectiveStance(currentType);
    }

    public void DeductPoints(int amount)
    {
        if (amount <= StanceManager.Instance.GetPointsForRespectiveStance(currentType))
            StanceManager.Instance.ChangePointToRespectiveStance(currentType, -amount);
    }

    private void ReadyAllSkills()
    {

        foreach (SkillSO skillSO in allSkillSOs)
        {
            if (!skillSlotDictionary.ContainsKey(skillSO.skillName))
            {
                SkillSlot newSlot = InstantiateSkillSlot(skillSO.category);

                newSlot.Setup(skillSO);

                skillSlotDictionary[skillSO.skillName] = newSlot;
            }
        }

        foreach (SkillSO skillSO in allSkillSOs)
        {

            SkillSlot slot = skillSlotDictionary[skillSO.skillName];

            foreach (SkillPrerequisite prerequisite in skillSO.prerequisites)
            {
                SkillSlot prerequisiteSlot = null;

                if (!skillSlotDictionary.ContainsKey(prerequisite.skillSO.skillName))
                {
                    prerequisiteSlot = InstantiateSkillSlot(prerequisite.skillSO.category);

                    prerequisiteSlot.Setup(prerequisite.skillSO);

                    skillSlotDictionary[prerequisite.skillSO.skillName] = prerequisiteSlot;
                }
                else
                {
                    prerequisiteSlot = skillSlotDictionary[prerequisite.skillSO.skillName];
                }

                ReslovedPrerequisiteSkillSlots rspp = new ReslovedPrerequisiteSkillSlots();
                rspp.slot = prerequisiteSlot;
                rspp.requiredLevel = prerequisite.requiredLevel;

                slot.prerequisiteSkillSlots.Add(rspp);
            }
        }

        CheckForUnlockingSkills();
    }

    public void TryUpgradeSkill(SkillSlot slot)
    {
        // Debug.Log("TryUpgradeSkill called for: " + slot.skillSO.skillName);

        bool isUpgraded = slot.UpgradeSkill();
        if (isUpgraded)
        {
            CheckForUnlockingSkills();
            UpdateUI();
        }
    }

    public void CheckForUnlockingSkills()
    {
        foreach (SkillSlot slot in skillSlotDictionary.Values)
        {
            if (slot.isUnlocked) continue;

            if (slot.CanUnlockSkill())
            {
                slot.UnlockSkill();
                slot.AddOnClickToUpgrade();
            }
        }
    }

    public void ShowSkills(SkillCategory type)
    {
        if (type == SkillCategory.Combat)
        {
            combatPanel.gameObject.SetActive(true);
            archeryPanel.gameObject.SetActive(false);
            title.text = "Combat Skills";
            currentType = type;
        }
        else
        {
            combatPanel.gameObject.SetActive(false);
            archeryPanel.gameObject.SetActive(true);
            title.text = "Archery Skills";
            currentType = type;
        }

        UpdatePointsUI();
    }

    private SkillSlot InstantiateSkillSlot(SkillCategory type)
    {
        return type == SkillCategory.Combat ? Instantiate(skillSlotPrefab, combatPanel) : Instantiate(skillSlotPrefab, archeryPanel);
    }

    private void UpdateUI()
    {
        ShowSkills(SkillCategory.Combat);
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        string text = currentType == SkillCategory.Combat ? "Combat Points" : "Archery Points";
        pointsText.text = text + " : " + StanceManager.Instance.GetPointsForRespectiveStance(currentType);
    }
}
