using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestSO> quests = new List<QuestSO>();
    public static event Action<string, int> Message;
    public int messageTimer = 5;
    public Sprite activeMine;
    public SpriteRenderer mine1;
    public SpriteRenderer mine2;
    public SpriteRenderer mine3;

    public Dictionary<string, bool> killedIds = new Dictionary<string, bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (SaveManager.Instance.isNewGame)
                ResetAllQuestSOs();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ResetAllQuestSOs()
    {
        foreach (QuestSO questSO in quests)
        {
            questSO.questState = QuestState.None;
            foreach (EnemyRequirement enemyRequirement in questSO.enemyRequirements)
            {
                enemyRequirement.killCount = 0;
            }
        }
    }

    public void SetQuestData(QuestData questData)
    {
        foreach (QuestSO questSO in quests)
        {
            if (questSO.label == questData.questName)
            {
                questSO.questState = questData.questState;

                int len = Mathf.Min(questData.killCounts.Count, questSO.enemyRequirements.Count);
                for (int i = 0; i < len; i++)
                {
                    questSO.enemyRequirements[i].killCount = questData.killCounts[i];
                }
                return;
            }
        }
    }

    public void ApplyQuestLocks()
    {
        foreach (QuestSO questSO in quests)
        {
            if (questSO.label == "Clear the road up ahead.")
            {
                if (questSO.questState == QuestState.Accepted || questSO.questState == QuestState.Completed)
                    SetLockState("Quest1Lock", false);

                if (questSO.questState == QuestState.Completed)
                {
                    SetLockState("Quest1AfterStartLock", false);
                    Enemy_Random_Spawn.Instance.isRandomSpawnAllowed = true;
                }

            }
            else if (questSO.label == "Collect me 10 wood logs." && questSO.questState == QuestState.Completed)
            {
                StanceManager.Instance.UnlockArcherStance();
            }
        }

        NavMeshSurface surface = GameObject.FindFirstObjectByType<NavMeshSurface>();
        if (surface != null)
            surface.BuildNavMesh();
    }

    private void SetLockState(string tag, bool colliderEnabled)
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tag);
        if (obj == null) return;

        var collider = obj.GetComponent<TilemapCollider2D>();
        if (collider != null) collider.enabled = colliderEnabled;

        var modifier = obj.GetComponent<NavMeshModifier>();
        if (modifier != null) modifier.enabled = colliderEnabled;
    }

    public void SetKilledEnemyData(KilledEnemy killedEnemy)
    {
        killedIds[killedEnemy.id] = killedEnemy.alreadyCounted;
    }

    private void OnEnable()
    {
        Enemy_Health.OnEnemyKilled += HandleKilled;
    }

    private void OnDisable()
    {
        Enemy_Health.OnEnemyKilled -= HandleKilled;
    }

    public void HandleKilled(QuestSO questSO)
    {
        bool changeHappened = false;
        int count = 0;

        foreach (EnemyRequirement requirement in questSO.enemyRequirements)
        {
            if (requirement.ids == null || requirement.ids.Length == 0) continue;

            foreach (string id in requirement.ids)
            {
                if (killedIds.TryGetValue(id, out bool alreadyCounted) && !alreadyCounted)
                {
                    requirement.Progress();
                    killedIds[id] = true;
                    changeHappened = true;
                    count++;
                }
            }
        }

        if (changeHappened)
        {
            bool questCompletion = questSO.IsQuestCompleted();
            if (questCompletion)
            {
                if (questSO.label == "Reclaim the mine 1.")
                    mine1.sprite = activeMine;
                questSO.MarkQuestCompleted();
                string text = $"{count} Quest Task Completed";
                Message?.Invoke(text, messageTimer);
            }
        }
    }

    private void HandleKilled(Enemy_Type type, string id)
    {
        if (id != null && id != "" && !killedIds.ContainsKey(id)) killedIds[id] = false;
        foreach (QuestSO questSO in quests)
        {
            if (questSO.questState == QuestState.Accepted)
            {
                bool changeHappened = false;
                int count = 0;
                foreach (EnemyRequirement requirement in questSO.enemyRequirements)
                {
                    bool isThereId = requirement.ids != null && requirement.ids.Length > 0;
                    bool idMatch = requirement.type == type && (!isThereId || requirement.ids.Contains(id));

                    if (!idMatch) continue;


                    if (isThereId)
                    {
                        if (killedIds.TryGetValue(id, out bool alreadyCounted) && alreadyCounted) continue;
                        killedIds[id] = true;
                    }

                    requirement.Progress();
                    changeHappened = true;
                    count++;

                }
                if (changeHappened)
                {
                    bool questCompletion = questSO.IsQuestCompleted();
                    if (questCompletion)
                    {
                        if (questSO.label == "Reclaim the mine 1.")
                            mine1.sprite = activeMine;
                        else if (questSO.label == "Reclaim the mine 2.")
                            mine2.sprite = activeMine;
                        else if (questSO.label == "Reclaim the mine 3.")
                            mine3.sprite = activeMine;
                        questSO.MarkQuestCompleted();
                        string text = $"{count} Quest Task Completed";
                        Message?.Invoke(text, messageTimer);
                    }
                }
            }
        }
    }
}
