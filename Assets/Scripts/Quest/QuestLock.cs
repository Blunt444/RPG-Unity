using System;
using UnityEngine;

public class QuestLock : MonoBehaviour
{
    public string questLabel;
    public int messageTimer = 3;
    public static event Action<string, int> Message;

    private void OnCollisionEnter2D(Collision2D  collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        QuestSO quest = QuestManager.Instance.quests.Find(q => questLabel == q.label);
        Debug.Log($"Looking for: '{questLabel}', found: {(quest != null ? quest.label : "NULL")}");

        if (quest == null) return;

        string text = "";

        switch (quest.questState)
        {
            case QuestState.None:
                text = "You need to accept this quest to pass.";
                break;
            case QuestState.Accepted:
                text = "Complete the quest to clear this path.";
                break;
            case QuestState.Declined:
                text = "You need to accept this quest to pass.";
                break;
            default:
                break;
        }

        if (text != null && text != "")
            Message?.Invoke(text, messageTimer);
    }

}
