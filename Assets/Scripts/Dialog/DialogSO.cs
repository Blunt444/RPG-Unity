using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class DialogSO : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Conditional Requirements (Optional)")]
    public ActorSO[] requiredNPCs;


    public bool IsConditionMet()
    {
        if(requiredNPCs.Length > 0)
        {
            foreach (ActorSO npc in requiredNPCs)
            {
                if (!DialogHistoryTracker.Instance.HasSpokenWith(npc))
                {
                    return false;
                }
            }
        }

        return true;
    }

}

[Serializable]
public class DialogueLine
{
     public ActorSO speaker;
     [TextArea(3,5)] public string text;
     
}

[Serializable]
public class DialogueOption
{
    public string optionText;
    public DialogSO nextDialog;


}