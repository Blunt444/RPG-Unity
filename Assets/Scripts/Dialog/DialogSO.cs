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
    public QuestSO quest;
    public List<ActorSO> requiredActors = new List<ActorSO>();

    public int nextLineIndex = -1;
    public int questAcceptNextLineIndex = -1;
    public int questDeclineNextLineIndex = -1;
    // public bool spokeToRequiredActor = true;  //default to true so can advance it will be set to false in inspector so canTalkToNPC can change its value
    public int checkpointIndex = 0;
}

[Serializable]
public class DialogueTopic
{
    [TextArea(3, 5)] public string label;
    public TextFor textFor = TextFor.Topic; // just so it is easy to interpretup in the inspector
    public int nextLineIndex = -1;
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
public enum TextFor
{
    Topic,
    Quest
}