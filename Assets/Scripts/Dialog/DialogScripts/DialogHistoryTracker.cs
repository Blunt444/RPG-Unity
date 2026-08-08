using System.Collections.Generic;
using UnityEngine;

public class DialogHistoryTracker : MonoBehaviour
{
    public static DialogHistoryTracker Instance;

    private HashSet<ActorSO> talkedNpcs = new HashSet<ActorSO>();

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

    public void AddToTalkedNpc(ActorSO actorSO)
    {
        if (actorSO != null)
            talkedNpcs.Add(actorSO);
    }

    public bool HasTalkedToNpc(ActorSO actorSO)
    {
        return actorSO != null && talkedNpcs.Contains(actorSO);
    }

    public bool CanTalkToNpc(NPC_Talk npc)
    {

        if (npc == null || npc.requiredActors == null) return true;

        foreach (ActorSO requiredActor in npc.requiredActors)
        {
            if (requiredActor != null && !talkedNpcs.Contains(requiredActor))
            {
                return false;
            }
        }

        return true;
    }
}
