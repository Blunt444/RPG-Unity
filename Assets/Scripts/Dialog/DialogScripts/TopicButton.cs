using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopicButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMP_Text topicText;
    [SerializeField] private GameObject questionMark;

    private string label;
    private int targetIndex;
    private int currentIndex;
    private QuestState questState = QuestState.None;
    private Action<int> onSelectSimpleCallBack;
    private Action<int, QuestState, int> onSelectQuestCallBack;

    private void Awake()
    {
        if (questionMark == null)
        {
            questionMark = transform.Find("QuestionMark").gameObject;
            topicText = transform.Find("Label").gameObject.GetComponent<TMP_Text>();
        }
        if (questionMark != null)
        {
            questionMark.SetActive(false);
        }
    }

    public void SetUp(string label, int targetIndex, Action<int> onSelect)
    {
        this.label = label;
        topicText.text = label;
        this.targetIndex = targetIndex;
        onSelectSimpleCallBack = onSelect;
        onSelectQuestCallBack = null;
    }

    public void SetUp(string label, int targetIndex, Action<int, QuestState, int> onSelect, int currentIndex, QuestState questState)
    {
        this.label = label;
        topicText.text = label;
        this.targetIndex = targetIndex;
        this.currentIndex = currentIndex;
        this.questState = questState;
        onSelectQuestCallBack = onSelect;
        onSelectSimpleCallBack = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onSelectSimpleCallBack != null)
            onSelectSimpleCallBack?.Invoke(targetIndex);
        else
            onSelectQuestCallBack?.Invoke(targetIndex, questState, currentIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (questionMark != null)
        {
            questionMark.SetActive(true);
        }
        topicText.text = $"<u>{label}</u>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (questionMark != null)
        {
            questionMark.SetActive(false);
        }
        topicText.text = label;
    }


}
