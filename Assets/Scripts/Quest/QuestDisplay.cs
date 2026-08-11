using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public Transform questBox;
    public GameObject questPrefab;
    public void Display()
    {
        foreach (QuestSO questSO in QuestManager.Instance.quests)
        {
            if (questSO.questState == QuestState.Accepted || questSO.questState == QuestState.Completed)
            {
                GameObject obj = Instantiate(questPrefab, questBox);
            }
        }
    }
}
