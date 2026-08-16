using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public List<QuestSO> allQuests;
    public List<ItemSO> allItems;
    public List<SkillSO> allSkills;

    public string savePath = Path.Combine(Application.persistentDataPath, "Saves");


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        else Destroy(gameObject);
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();

        DateTime now = DateTime.Now;
        data.timestamp = now.ToString("yyyy-MM-dd HH:mm:ss");

        data.gold = InventoryManager.Instance.gold;
        data.warriorStancePoint = StanceManager.Instance.GetPointsForRespectiveStance(SkillCategory.Combat);
        data.archeryStancePoint = StanceManager.Instance.GetPointsForRespectiveStance(SkillCategory.Archery);
        data.currentArrowCount = ArrowQuantityManager.Instance.GetQuantity();
        data.maxArrowCount = ArrowQuantityManager.Instance.GetMaxArrowCount();
        data.currentHealth = StatsManager.Instance.currentHealth;
        data.maxHealth = StatsManager.Instance.maxHealth;
        data.maxGuardHit = StatsManager.Instance.maxGuardHitNegate;
        data.lastRespawnPoint = RespawnPointManager.Instance.GetCurrentRespawnPointId();

        Dictionary<string, float> archeryData = LevelSystem.Instance.GetValuesFromSystem(PlayerStance.Archer);
        data.archeryData = new ExpData
        {
            lvl = (int)archeryData["level"],
            currentExp = (int)archeryData["currentExp"],
            expToLevel = (int)archeryData["expToLevel"],
            expGrowthMultiplier = archeryData["expGrowthMultiplier"]
        };

        Dictionary<string, float> warriorData = LevelSystem.Instance.GetValuesFromSystem(PlayerStance.Warrior);
        data.warriorData = new ExpData
        {
            lvl = (int)warriorData["level"],
            currentExp = (int)warriorData["currentExp"],
            expToLevel = (int)warriorData["expToLevel"],
            expGrowthMultiplier = warriorData["expGrowthMultiplier"]
        };

        foreach (InventorySlot slot in InventoryManager.Instance.inventorySlots)
        {
            if (slot.itemSO == null) continue;
            data.inventory.Add(new InventorySlotData { itemName = slot.itemSO.itemName, quantity = slot.quantity });
        }

        foreach (QuestSO questSO in QuestManager.Instance.quests)
        {
            QuestData entry = new QuestData { questName = questSO.label, questState = questSO.questState };
            entry.killCounts = new List<int>();

            foreach (EnemyRequirement enemyRequirement in questSO.enemyRequirements)
            {
                entry.killCounts.Add(enemyRequirement.killCount);
            }

            data.quests.Add(entry);
        }

        foreach (var skill in SkillTreeManager.Instance.GetAllSkills())
        {
            data.skills.Add(
                new SkillData
                {
                    skillName = skill.Value.skillSO.skillName,
                    isUnlocked = skill.Value.isUnlocked,
                    lvl = skill.Value.currentLevel
                }
            );
        }

        data.talkedNpcs = DialogHistoryTracker.Instance.GetTalkedNPCNames();

        string json = JsonUtility.ToJson(data, true);

        string fileName = $"save_{now:yyyyMMdd_HHmmss_fff}.json";
        string fullPath = Path.Combine(savePath, fileName);

        File.WriteAllText(fullPath, json);
    }
}

[Serializable]
public class SaveData
{
    public string timestamp;
    public int gold;
    public ExpData warriorData;
    public ExpData archeryData;
    public int warriorStancePoint;
    public int archeryStancePoint;
    public int currentArrowCount;
    public int maxArrowCount;
    public int currentHealth;
    public int maxHealth;
    public int maxGuardHit;
    public string lastRespawnPoint;
    public List<InventorySlotData> inventory = new();
    public List<QuestData> quests = new();
    public List<SkillData> skills = new();
    public List<string> talkedNpcs = new();
}

[Serializable]
public class InventorySlotData
{
    public string itemName;
    public int quantity;
}

[Serializable]
public class QuestData
{
    public string questName;
    public QuestState questState;
    public List<int> killCounts;
}

[Serializable]
public class SkillData
{
    public string skillName;
    public int lvl;
    public bool isUnlocked;
}

[Serializable]
public class ExpData
{
    public int lvl;
    public int currentExp;
    public int expToLevel;
    public float expGrowthMultiplier;
}