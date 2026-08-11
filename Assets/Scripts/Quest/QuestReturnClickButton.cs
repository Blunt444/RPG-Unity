using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestRetrunClickButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Action onClick;
    public TMP_Text box;
    public string text;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick?.Invoke();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        box.text = $"<u>{text}</u>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        box.text = text;
    }

    public void Setup(Action onClick)
    {
        this.onClick = onClick;
    }

}
