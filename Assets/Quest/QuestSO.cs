using System;
using UnityEngine;

public class QuestSO : ScriptableObject
{
    public string label;
    [NonSerialized] public QuestState questState;
}

public enum QuestState { None, Accepted, Declined, Completed };