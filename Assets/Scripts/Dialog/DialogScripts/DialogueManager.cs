using System.Collections.Generic;
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
    public GameObject topicItem;
    public Transform topicBox;
    public NPC_Talk npc;
    public List<DialogSO> dialogSOs = new List<DialogSO>();


    public CanvasGroup dialogCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (SaveManager.Instance.isNewGame)
                ResetAllDialogSOs();
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
        // Debug.Log("Canvas state : " + isOpened);
    }

    private void ResetAllDialogSOs()
    {
        foreach (DialogSO dialogSO in dialogSOs)
        {
            dialogSO.returnStartIndex = 0;
        }
    }

    public int GetStartIndex(DialogSO dialogSO)
    {
        return dialogSO.returnStartIndex;
    }

    public int EndConversation(DialogSO dialogSO)
    {
        if (isOpened)
        {
            ToggleVisibility();
            npc = null;
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

        bool isThereTopics = CheckForTopics(dialogSO, currentIndex);

        ClearTopicInstance();

        if (isThereTopics)
        {
            topicBox.gameObject.SetActive(true);
            CreateTopicInstance(line.topics);
        }
        else
        {
            topicBox.gameObject.SetActive(false);
        }

        if (line.quest != null)
        {
            topicBox.gameObject.SetActive(true);
            CreateQuestAcceptReject(line.questAcceptNextLineIndex, line.questDeclineNextLineIndex, currentIndex);
        }
        else
        {
            topicBox.gameObject.SetActive(false);
        }

        dialogSO.returnStartIndex = line.checkpointIndex;

    }

    public void CreateQuestAcceptReject(int acceptNextLineIndex, int declineNextLineIndex, int currentIndex)
    {
        GameObject accept = Instantiate(topicItem, topicBox);
        TopicButton acceptBtn = accept.GetComponent<TopicButton>();
        acceptBtn.SetUp("Accept", acceptNextLineIndex, OnButtonClicked, currentIndex, QuestState.Accepted);

        GameObject decline = Instantiate(topicItem, topicBox);
        TopicButton declineBtn = decline.GetComponent<TopicButton>();
        declineBtn.SetUp("Decline", declineNextLineIndex, OnButtonClicked, currentIndex, QuestState.Declined);

    }

    public void CreateTopicInstance(List<DialogueTopic> topics)
    {
        foreach (DialogueTopic topic in topics)
        {
            GameObject obj = Instantiate(topicItem, topicBox);
            TopicButton button = obj.GetComponent<TopicButton>();

            button.SetUp(topic.label, topic.nextLineIndex, OnButtonClicked);
        }
    }
    public bool CheckForTopics(DialogSO dialogSO, int currentIndex)
    {
        return dialogSO.lines[currentIndex].topics.Count > 0;
    }

    public void ClearTopicInstance()
    {
        foreach (Transform child in topicBox.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnButtonClicked(int nextLineIndex, QuestState questState, int currentIndex)
    {
        if (npc == null) return;


        npc.dialogSO.lines[currentIndex].quest.questState = questState;

        if (questState == QuestState.Accepted) npc.questSO = npc.dialogSO.lines[currentIndex].quest;

        Debug.Log(npc.dialogSO.lines[currentIndex].quest.questState);

        if (nextLineIndex == -1)
        {
            npc.SetLineIndex(EndConversation(npc.dialogSO));
        }
        else
        {
            npc.SetLineIndex(nextLineIndex);
            DisplayDialogue(npc.dialogSO, nextLineIndex);
        }
    }

    public void OnButtonClicked(int nextLineIndex)
    {
        if (npc == null) return;

        if (nextLineIndex == -1)
        {
            npc.SetLineIndex(EndConversation(npc.dialogSO));
        }
        else
        {
            npc.SetLineIndex(nextLineIndex);
            DisplayDialogue(npc.dialogSO, nextLineIndex);
        }
    }
}
