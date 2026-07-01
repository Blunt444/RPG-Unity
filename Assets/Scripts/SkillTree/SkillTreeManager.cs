using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public SkillSlot[] skillSlots;
    public TMP_Text pointsText;
    public int availablePoints;

    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandlePoints;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdatePoints;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandlePoints;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdatePoints;
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in skillSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
        }
    }

    private void Start()
    {
        foreach (SkillSlot slot in skillSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailablePoints(slot));
        }
        UpdatePoints(0);
    }

    private void CheckAvailablePoints(SkillSlot slot)
    {

        if (availablePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }

    public void HandlePoints(SkillSlot skillSlot)
    {
        if (availablePoints > 0)
        {
            UpdatePoints(-1);
        }
    }
    public void UpdatePoints(int amount)
    {
        availablePoints += amount;
        pointsText.text = "Skill Points: " + availablePoints;
    }
}
