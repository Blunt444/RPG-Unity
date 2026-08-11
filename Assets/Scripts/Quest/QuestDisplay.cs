using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public static QuestDisplay Instance;
    public Transform questBox;
    public GameObject questPrefab;
    public Transform questContainer;
    public Transform questInfoContainer;
    public CanvasGroup canvas;
    public GridLayoutGroup questGrid;
    public TMP_Text noQuestText;

    private bool questOpen = false;

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
    }

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

        foreach (Transform child in questContainer.transform)
        {
            Destroy(child.gameObject);
        }

        bool hasVisibleQuest = false;


        foreach (QuestSO questSO in QuestManager.Instance.quests)
        {
            if (questSO.questState == QuestState.Accepted || questSO.questState == QuestState.Completed)
            {
                QuestBox box = Instantiate(questPrefab, questBox).GetComponent<QuestBox>();
                box.Setup(questSO, OnQuestClick);
                hasVisibleQuest = true;
            }
        }

        noQuestText.gameObject.SetActive(!hasVisibleQuest);
        questGrid.enabled = hasVisibleQuest;
    }

    public void OnQuestClick(QuestSO questSO)
    {
        if (questContainer == null || questInfoContainer == null) return;

        questContainer.gameObject.SetActive(false);
        questInfoContainer.gameObject.SetActive(true);
        QuestInfo.Instance.Setup(questSO);
    }


}
