using System;
using System.Collections.Generic;
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

    public List<DialogueTopic> topics = new List<DialogueTopic>();

    public int nextLineIndex = -1;
    public int checkpointIndex = 0;
}

[Serializable]
public class DialogueTopic
{
    [TextArea(3, 5)] public string label;
    [TextArea(3, 5)] public string text;

    public int nextLineIndex = 0;
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