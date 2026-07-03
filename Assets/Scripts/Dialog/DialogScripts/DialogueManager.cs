using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public bool isOpened = false;

    public CanvasGroup dialogCanvas;

    private void Awake()
    {
        if(Instance == null)
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

    public int nextLineIndex(DialogSO dialogSO, int currentIndex)
    {
        return dialogSO.lines[currentIndex].nextLineIndex;
    }
}
