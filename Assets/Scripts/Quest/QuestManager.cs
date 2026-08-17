using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestSO> quests = new List<QuestSO>();
    public static event Action<string, int> Message;
    public int messageTimer = 5;

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

    private void OnEnable()
    {
        Enemy_Health.OnEnemyKilled += HandleKilled;
    }

    private void OnDisable()
    {
        Enemy_Health.OnEnemyKilled -= HandleKilled;
    }

    private void HandleKilled(Enemy_Type type)
    {
        foreach (QuestSO questSO in quests)
        {
            if (questSO.questState == QuestState.Accepted)
            {
                bool changeHappened = false;
                int count = 0;
                foreach (EnemyRequirement requirement in questSO.enemyRequirements)
                {
                    if (requirement.type == type)
                    {
                        requirement.Progress();
                        changeHappened = true;
                        count++;
                    }
                }
                if (changeHappened)
                {
                    bool questCompletion = questSO.IsQuestCompleted();
                    if (questCompletion)
                    {
                        questSO.MarkQuestCompleted();
                        string text = $"{count} Quest Completed";
                        Message?.Invoke(text, messageTimer);
                    }
                }
            }
        }
    }
}
