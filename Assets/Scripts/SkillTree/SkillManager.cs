using UnityEngine;

public class SkillManager : MonoBehaviour
{
    // private PlayerStatsUpgrade playerStatsUpgrade;

    // private void Awake()
    // {
    //     playerStatsUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsUpgrade>();
    // }

    // private void OnEnable()
    // {
    //     SkillSlot.OnAbilityPointSpent += HandleSkillUpgrade;
    // }
    // private void OnDisable()
    // {
    //     SkillSlot.OnAbilityPointSpent -= HandleSkillUpgrade;
    // }

    // private void HandleSkillUpgrade(SkillSlot slot)
    // {
    //     string name = slot.skillSO.skillName;

    //     Debug.Log("Health" + name);

    //     switch (name)
    //     {
    //         case "Max Health Booster":
    //             playerStatsUpgrade.UpdateMaxHealth(1);
    //             break;
    //         default:
    //             Debug.LogWarning("UNKOWN SKILL : " + name);
    //             break;
    //     }
    // }
}
