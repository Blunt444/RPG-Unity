using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestSO> quests = new List<QuestSO>();

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
            foreach (EnemyRequirement requirement in questSO.enemyRequirements)
            {
                if (requirement.type == type)
                {
                    requirement.Progress();
                }
            }
        }
    }
}
