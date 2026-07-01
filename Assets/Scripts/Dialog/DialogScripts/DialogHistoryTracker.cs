using System.Collections.Generic;
using UnityEngine;

public class DialogHistoryTracker : MonoBehaviour
{
    public static DialogHistoryTracker Instance;

    private readonly List<ActorSO> spokenNPCs = new List<ActorSO>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RecordNPC(ActorSO actorSO)
    {
        spokenNPCs.Add(actorSO);
    }

    public bool HasSpokenWith(ActorSO actorSO)
    {
        foreach (ActorSO npc in spokenNPCs)
        {
            if(actorSO == npc) return true;
        }
        return false;
    }

}
