using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public List<SkillSO> allSkills;
    public SkillSlot skillSlotPrefab;
    public Transform combatPanel;
    public Transform magicPanel;
    public TMP_Text pointsText;
    public int availablePoints;


    private Dictionary<SkillSO, SkillSlot> slotDic = new Dictionary<SkillSO, SkillSlot>();

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandlePoints;
        ExpManager.OnLevelUp += UpdatePoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandlePoints;
        ExpManager.OnLevelUp -= UpdatePoints;
    }

    private void Start()
    {
        foreach (SkillSO skillSO in allSkills)
        {
            Transform parent = skillSO.category == SkillCategory.Combat ? combatPanel : magicPanel;
            SkillSlot slot = Instantiate(skillSlotPrefab, parent);
            slot.Setup(skillSO);
            slotDic.Add(skillSO, slot);

        }
        foreach (var item in slotDic)
        {
            SkillSO skillSO = item.Key;
            SkillSlot slot = item.Value;

            foreach (SkillPrerequisite prereq in skillSO.prerequisites)
            {
                if (slotDic.TryGetValue(prereq.skillSO, out SkillSlot prereqSlot))
                {
                    slot.prerequisiteSkillSlots.Add(
                        new ReslovedPrerequisiteSkillSlots
                        {
                            slot = prereqSlot,
                            requiredLevel = prereq.requiredLevel
                        }
                    );
                }
            }
        }
        foreach (var slot in slotDic.Values)
        {
            if (slot.skillSO.prerequisites.Count == 0) slot.Unlock();
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }
        UpdatePoints(0);
    }

    private void CheckAvailablePoints(SkillSlot slot)
    {

        if (availablePoints <= 0) return;

        int before = slot.currentLevel;
        slot.TryUpgradeSkill();

        int after = slot.currentLevel;

        if (before < after)
        {
            UpdatePoints(-1);
        }
    }

    public void HandlePoints(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in slotDic.Values)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }

    public void UpdatePoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = "Skill Points: " + availablePoints;
    }
}
