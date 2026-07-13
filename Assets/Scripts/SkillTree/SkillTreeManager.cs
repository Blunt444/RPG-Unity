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
    public int availablePoints;

    [NonSerialized] private Dictionary<string, SkillSlot> skillSlotDictionary = new Dictionary<string, SkillSlot>();

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

    public int GetCurrentPoints()
    {
        return availablePoints;
    }

    public void DeductPoints(int amount)
    {
        if (amount <= availablePoints)
            availablePoints -= amount;
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
    }

    private SkillSlot InstantiateSkillSlot(SkillCategory type)
    {
        return type == SkillCategory.Combat ? Instantiate(skillSlotPrefab, combatPanel) : Instantiate(skillSlotPrefab, archeryPanel);
    }

    private void UpdateUI()
    {
        UpdatePointsUI();
    }

    private void UpdatePointsUI()
    {
        pointsText.text = availablePoints.ToString();
    }
}
