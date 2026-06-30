using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager Instance;

    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceBtns;
    public bool isDialogueActive;

    private DialogSO currentDialog;
    private int dialogIdx;

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
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (Button btn in choiceBtns)
        {
            btn.gameObject.SetActive(false);
        }
    }


    public void StartDialogue(DialogSO dialogSO)
    {
        isDialogueActive = true;
        dialogIdx = 0;
        currentDialog = dialogSO;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogIdx < currentDialog.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            ShowChoices();
        }
    }

    private void EndDialogue()
    {
        ClearChoices();

        isDialogueActive = false;
        currentDialog = null;

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowChoices()
    {
        ClearChoices();

        if (currentDialog.options.Length > 0)
        {
            for (int i = 0; i < currentDialog.options.Length; i++)
            {
                var option = currentDialog.options[i];
                choiceBtns[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceBtns[i].gameObject.SetActive(true);

                choiceBtns[i].onClick.AddListener(()=> ChooseOption(option.nextDialog));
            }
        }
        else
        {
            choiceBtns[0].gameObject.GetComponentInChildren<TMP_Text>().text = "End";
            choiceBtns[0].onClick.AddListener(()=> EndDialogue());
            choiceBtns[0].gameObject.SetActive(true);
        }
    }

    private void ChooseOption(DialogSO dialogSO)
    {
        if(dialogSO == null) EndDialogue();
        else
        {
            ClearChoices();
            StartDialogue(dialogSO);
        }
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialog.lines[dialogIdx];

        portrait.sprite = line.speaker.portrait;
        actorName.text = line.speaker.actorName;

        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        dialogIdx++;
    }

    private void ClearChoices()
    {
        foreach(Button btn in choiceBtns)
        {
            btn.gameObject.SetActive(false);
            btn.onClick.RemoveAllListeners();
        }
    }
}
