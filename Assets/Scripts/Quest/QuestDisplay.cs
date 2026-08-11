using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public Transform questBox;
    public GameObject questPrefab;
    public Transform questContainer;
    public Transform questInfoContainer;

    public void Display()
    {
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
