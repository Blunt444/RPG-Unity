using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO")]
public class DialogSO : ScriptableObject
{
    public DialogueLine[] lines;
}

[Serializable]
public class DialogueLine
{
     public ActorSO speaker;
     [TextArea(3,5)] public string text;

}