using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public string sceneName;

    public string savePath;
    public static event Action<string, LoadButtonAction, bool> buttonResponse;
    private SaveData data;
    public bool isNewGame = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SetSavePath();
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }
        else Destroy(gameObject);
    }

    private void SetSavePath()
    {
        savePath = Path.Combine(Application.persistentDataPath, "Saves");
    }

    public bool SaveGame()
    {
        try
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

            foreach (DialogSO dialogSO in DialogueManager.Instance.dialogSOs)
            {
                DialogData dialogData = new DialogData { id = dialogSO.id, index = dialogSO.returnStartIndex };
                data.dialogDatas.Add(dialogData);
            }

            foreach (var loot in InventoryManager.Instance.loots)
            {
                data.loots.Add(new LootData { id = loot.Key, lootInfo = loot.Value });
            }

            foreach (TreeScript tree in TreeManager.Instance.trees)
            {
                TreeData treeData = new TreeData { id = tree.id, isDead = tree.isDead };
                data.treeDatas.Add(treeData);
            }

            data.talkedNpcs = DialogHistoryTracker.Instance.GetTalkedNPCNames();

            string json = JsonUtility.ToJson(data, true);

            string fileName = $"save_{now:yyyyMMdd_HHmmss_fff}.json";
            string fullPath = Path.Combine(savePath, fileName);

            File.WriteAllText(fullPath, json);

            return true;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }

    }

    public List<string> GetAllSaves()
    {
        string[] files = Directory.GetFiles(savePath, "*.json");

        List<string> fileNames = new List<string>();

        foreach (string file in files)
        {
            fileNames.Add(Path.GetFileNameWithoutExtension(file));
        }

        return fileNames;
    }

    private void OnEnable()
    {
        LoadButton.LoadButtonClicked += ButtonClicked;
    }

    private void OnDisable()
    {
        LoadButton.LoadButtonClicked -= ButtonClicked;
    }

    public void ButtonClicked(LoadButtonAction action, string fileName)
    {
        if (action == LoadButtonAction.Load)
        {
            LoadFile(fileName);
        }
        else
        {
            DeleteFile(fileName);
        }
    }

    public void LoadFile(string fileName)
    {
        string fullFileName = fileName + ".json";
        string path = Path.Combine(savePath, fullFileName);
        if (File.Exists(path))
        {
            buttonResponse?.Invoke(fileName, LoadButtonAction.Load, true);
        }
        else
        {
            buttonResponse?.Invoke(fileName, LoadButtonAction.Load, false);
        }
    }

    public void LoadGame(string fileName)
    {
        string fullFileName = fileName + ".json";
        string path = Path.Combine(savePath, fullFileName);

        string json = File.ReadAllText(path);
        data = JsonUtility.FromJson<SaveData>(json);

        SceneManager.sceneLoaded -= OnGameplaySceneLoaded;
        SceneManager.sceneLoaded += OnGameplaySceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    private void OnGameplaySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnGameplaySceneLoaded;
        StartCoroutine(DelayApplySaveData());
    }

    private IEnumerator DelayApplySaveData()
    {
        yield return null;
        ApplySaveData(data);
    }

    public void ApplySaveData(SaveData data)
    {
        isNewGame = false;
        InventoryManager.Instance.SetGold(data.gold);
        StanceManager.Instance.SetPointsToStance(SkillCategory.Combat, data.warriorStancePoint);
        StanceManager.Instance.SetPointsToStance(SkillCategory.Archery, data.archeryStancePoint);
        ArrowQuantityManager.Instance.SetArrowData(data.currentArrowCount, data.maxArrowCount);
        RespawnPointManager.Instance.SetRespawnPoint(data.lastRespawnPoint);
        StatsManager.Instance.SetSaveData(data.currentHealth, data.maxHealth, data.maxGuardHit);
        DeathCanvasScript.Instance.SpawnAtCheckPoint();

        foreach (InventorySlotData slotData in data.inventory)
        {
            Debug.Log($"{slotData.itemName} x{slotData.quantity}");
            InventoryManager.Instance.AddItem(slotData.itemName, slotData.quantity);
        }

        foreach (string actorName in data.talkedNpcs)
        {
            DialogHistoryTracker.Instance.AddToTalkedNpc(actorName);
        }

        foreach (QuestData questData in data.quests)
        {
            QuestManager.Instance.SetQuestData(questData);
        }

        foreach (DialogData dialogData in data.dialogDatas)
        {
            DialogueManager.Instance.SetSaveDialogData(dialogData);
        }

        foreach (TreeData treeData in data.treeDatas)
        {
            TreeManager.Instance.SetSaveTreeData(treeData);
        }

        foreach (LootData lootData in data.loots)
        {
            InventoryManager.Instance.AddLootData(lootData);
        }

        foreach (SkillData skillData in data.skills)
        {
            SkillTreeManager.Instance.SetSKillData(skillData);
        }
        SkillTreeManager.Instance.CheckForUnlockingSkills();

        LevelSystem.Instance.GetAndSetValueInSystem(PlayerStance.Warrior, data.warriorData);
        LevelSystem.Instance.GetAndSetValueInSystem(PlayerStance.Archer, data.archeryData);
    }

    public void DeleteFile(string fileName)
    {
        string fullFileName = fileName + ".json";
        string path = Path.Combine(savePath, fullFileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            buttonResponse?.Invoke(fileName, LoadButtonAction.Delete, true);
        }
        else
        {
            buttonResponse?.Invoke(fileName, LoadButtonAction.Delete, false);
        }
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
    public List<DialogData> dialogDatas = new();
    public List<TreeData> treeDatas = new();
    public List<LootData> loots = new();
}

[Serializable]
public class InventorySlotData
{
    public string itemName;
    public int quantity;
}

[Serializable]
public class DialogData
{
    public string id;
    public int index;
}

[Serializable]
public class LootData
{
    public string id;
    public LootInfo lootInfo;
}

[Serializable]
public class TreeData
{
    public string id;
    public bool isDead;
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