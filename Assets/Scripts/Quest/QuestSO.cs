using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO")]
public class QuestSO : ScriptableObject
{
    [TextArea(3, 5)] public string label;
    [TextArea(3, 5)] public string about;
    public QuestState questState = QuestState.None;

    public List<Reward> rewards = new List<Reward>();

    public List<EnemyRequirement> enemyRequirements = new List<EnemyRequirement>();
    public List<CollectableRequirement> collectableRequirements = new List<CollectableRequirement>();
    public List<TalkToActor> talkToActors = new List<TalkToActor>();

    public bool IsQuestCompleted()
    {
        foreach (EnemyRequirement enemyRequirement in enemyRequirements)
        {
            if (!enemyRequirement.IsComplete()) return false;
        }
        foreach (CollectableRequirement collectableRequirement in collectableRequirements)
        {
            if (!collectableRequirement.IsComplete()) return false;
        }
        foreach (TalkToActor actor in talkToActors)
        {
            if (!actor.IsComplete()) return false;
        }

        return true;
    }

    public float Progress()
    {
        int total = enemyRequirements.Count + collectableRequirements.Count + talkToActors.Count;
        float progress = 0;

        foreach (EnemyRequirement enemyRequirement in enemyRequirements)
        {
            progress += enemyRequirement.ProgressRatio();
        }
        foreach (CollectableRequirement collectableRequirement in collectableRequirements)
        {
            progress += collectableRequirement.ProgressRatio();
        }
        foreach (TalkToActor actor in talkToActors)
        {
            progress += actor.ProgressRatio();
        }

        return (float)progress / total;
    }

    public void MarkQuestCompleted()
    {
        foreach (CollectableRequirement collectableRequirement in collectableRequirements)
        {
            collectableRequirement.ReduceItemCountQuestCompleted();
        }

        questState = QuestState.Completed;

        foreach (Reward reward in rewards)
        {
            InventoryManager.Instance.AddItem(reward.itemSO, reward.quantity);
        }
    }
}

public enum QuestState { None, Accepted, Declined, Completed };

[Serializable]
public abstract class QuestRequirement
{
    public abstract bool IsComplete();
    public abstract void Progress();
    public abstract float ProgressRatio();
}

[Serializable]
public class EnemyRequirement : QuestRequirement
{

    public Enemy_Type type;
    public int count;
    public int killCount;
    public string label;

    public override bool IsComplete()
    {
        return killCount >= count;
    }
    public override void Progress()
    {
        killCount++;
        Debug.Log(killCount);
    }
    public override float ProgressRatio()
    {
        return (float)killCount / count;
    }
}

[Serializable]
public class CollectableRequirement : QuestRequirement
{
    public ItemSO itemSO;
    public int count;
    public string label;

    public override bool IsComplete()
    {
        return InventoryManager.Instance.GetItemCount(itemSO) >= count;
    }

    public override void Progress()
    {
        throw new NotImplementedException();
    }

    public override float ProgressRatio()
    {
        return (float)InventoryManager.Instance.GetItemCount(itemSO) / count;
    }

    public void ReduceItemCountQuestCompleted()
    {
        InventoryManager.Instance.ReduceItemCount(itemSO, count);
    }
}

[Serializable]
public class TalkToActor : QuestRequirement
{
    public ActorSO actorSO;
    public string label;
    public override bool IsComplete()
    {
        return DialogHistoryTracker.Instance.HasTalkedToNpc(actorSO);
    }

    public override float ProgressRatio()
    {
        return IsComplete() ? 1f : 0f;
    }

    public override void Progress()
    {
        throw new NotImplementedException();
    }
}

[Serializable]
public class Reward
{
    public int quantity;
    public ItemSO itemSO;
}