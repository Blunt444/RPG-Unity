using TMPro;
using UnityEngine;

public class QuestInfo : MonoBehaviour
{
    public static QuestInfo Instance;
    public TMP_Text label;
    public TMP_Text about;
    public Transform requirement;
    public QuestSO questSO;
    public GameObject reqPrefab;

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

    public void Setup(QuestSO questSO)
    {
        label.text = questSO.label;
        about.text = questSO.about;
        this.questSO = questSO;

        foreach (Transform child in requirement.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (EnemyRequirement enemyRequirement in questSO.enemyRequirements)
        {
            QuestRequirementScript script = Instantiate(reqPrefab, requirement).GetComponent<QuestRequirementScript>();
            string progress = ": " + enemyRequirement.killCount + "/" + enemyRequirement.count;

            // Debug.Log(progress);

            script.Setup(enemyRequirement.label, progress, enemyRequirement.IsComplete());
        }

        foreach (CollectableRequirement collectableRequirement in questSO.collectableRequirements)
        {
            QuestRequirementScript script = Instantiate(reqPrefab, requirement).GetComponent<QuestRequirementScript>();
            string progress = ":" + InventoryManager.Instance.GetItemCount(collectableRequirement.itemSO) + "/" + collectableRequirement.count;
            script.Setup(collectableRequirement.label, progress, collectableRequirement.IsComplete());
        }

        foreach (TalkToActor talkToActor in questSO.talkToActors)
        {
            QuestRequirementScript script = Instantiate(reqPrefab, requirement).GetComponent<QuestRequirementScript>();
            string progress = talkToActor.IsComplete() ? "Yes" : "No";
            script.Setup(talkToActor.label, progress);
        }
    }

}
