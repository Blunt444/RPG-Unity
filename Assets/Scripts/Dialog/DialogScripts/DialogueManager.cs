using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool isOpened = false;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Image actorSprite;


    public CanvasGroup dialogCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ToggleVisibility()
    {
        if (isOpened)
        {
            dialogCanvas.alpha = 0;
            dialogCanvas.interactable = false;
            dialogCanvas.blocksRaycasts = false;

            isOpened = false;
        }
        else
        {
            dialogCanvas.alpha = 1;
            dialogCanvas.interactable = true;
            dialogCanvas.blocksRaycasts = true;

            isOpened = true;
        }
        Debug.Log("Canvas state : " + isOpened);
    }

    public int EndConversation(DialogSO dialogSO)
    {
        if (isOpened)
        {
            ToggleVisibility();
            return dialogSO.returnStartIndex;
        }
        return 0;
    }

    public int nextLineIndex(DialogSO dialogSO, int currentIndex)
    {
        return dialogSO.lines[currentIndex].nextLineIndex;
    }

    public void DisplayDialogue(DialogSO dialogSO, int currentIndex)
    {

        DialogueLine line = dialogSO.lines[currentIndex];

        actorName.text = line.speaker.actorName;
        actorSprite.sprite = line.speaker.portrait;

        dialogueText.text = line.text;
    }
}
