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
        // Debug.Log(actorSO);
    }

    public bool HasTalkedToNpc(ActorSO actorSO)
    {
        return actorSO != null && talkedNpcs.Contains(actorSO);
    }

    public List<string> GetTalkedNPCNames()
    {
        List<string> names = new List<string>();

        foreach (ActorSO actorSO in talkedNpcs)
        {
            names.Add(actorSO.actorName);
        }

        return names;
    }

    public bool CanShowNextLine(DialogueLine line)
    {

        if (line.requiredActors == null) return true;

        foreach (ActorSO requiredActor in line.requiredActors)
        {
            if (!HasTalkedToNpc(requiredActor))
            {
                return false;
            }
        }

        return true;
    }
}
