using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;

    private DialogSO currentDialog;
    private int dialogIdx;
    
}
