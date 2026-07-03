using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class DialogSO : ScriptableObject
{
    public DialogueLine[] lines;
    public int returnStartIndex = 0;
}

[Serializable]
public class DialogueLine
{
    public ActorSO speaker;
    [TextArea(3, 5)] public string text;
    public int nextLineIndex = -1;
}

[Serializable]
public class DialogueTopic
{
    public string label;
    [TextArea(3, 5)] public string text;
}

[Serializable]
public class DialogueChoice
{
    public string label;
    public int nextLineIndex;
    public ChoiceOutcome choiceOutcome = ChoiceOutcome.None;
    // public 
}

public enum ChoiceOutcome { None, Started, Completed, Declined }