using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class PauseButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textBox;
    public PauseButtonAction action;
    public static event Action<PauseButtonAction> onClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke(action);
        textBox.alpha = 1f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textBox.alpha = 0.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.alpha = 1f;
    }
}
