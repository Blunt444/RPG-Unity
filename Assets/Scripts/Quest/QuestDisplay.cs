using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public Transform questBox;
    public GameObject questPrefab;
    public Transform questContainer;
    public Transform questInfoContainer;
    public CanvasGroup canvas;

    private bool questOpen = false;

    private void Update()
    {
        if (Input.GetButtonDown("Quest"))
        {
            ToggleVisibility();
        }
    }

    private void ToggleVisibility()
    {
        if (questOpen)
        {
            canvas.alpha = 0;
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
            questOpen = false;
        }
        else
        {
            canvas.alpha = 1;
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
            questOpen = true;

            Display();
        }
    }

    public void Display()
    {

        foreach(Transform child in questContainer.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (QuestSO questSO in QuestManager.Instance.quests)
        {
            if (questSO.questState == QuestState.Accepted || questSO.questState == QuestState.Completed)
            {
                QuestBox box = Instantiate(questPrefab, questBox).GetComponent<QuestBox>();
                box.Setup(questSO, OnQuestClick);
            }
        }
    }

    public void OnQuestClick(QuestSO questSO)
    {
        if (questContainer == null) return;

        questContainer.gameObject.SetActive(false);
        questInfoContainer.gameObject.SetActive(true);

    }
}
