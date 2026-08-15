using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    
    public List<QuestSO> allQuests;
    public List<ItemSO> allItems;
    public List<SkillSO> allSkills;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}

[Serializable]
public class SaveData
{
    public int gold;
    public ExpData combatData = new();
    public ExpData archeryData = new();
    public int warriorStancePoint;
    public int archeryStancePoint;
    public int currentArrowCount;
    public int maxArrowCount;
    public int currentHealth;
    public int maxHealth;
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