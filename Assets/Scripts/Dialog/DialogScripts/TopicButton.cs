using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TopicButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private TMP_Text topicText;
    [SerializeField] private GameObject questionMark;

    private string label;
    private int targetIndex;
    private Action<int> onSelectCallBack;

    private void Awake()
    {
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
        onSelectCallBack = onSelect;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelectCallBack?.Invoke(targetIndex);
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
