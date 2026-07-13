using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;
    private PlayerStatsUpgrade playerStatsUpgrade;

    private void Awake()
    {
        playerStatsUpgrade = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatsUpgrade>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandleSkillUpgrade(SkillSlot slot)
    {
        string name = slot.skillSO.skillName;

        switch (name)
        {
            case "Max Health Booster":
                playerStatsUpgrade.UpdateMaxHealth(1);
                break;
            default:
                break;
        }
    }
}
