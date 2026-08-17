using UnityEngine;
using UnityEngine.EventSystems;
using System;
using TMPro;

public class PauseButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textBox;
    public PauseButtonAction action;
    public static event Action<PauseButtonAction> onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke(action);
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
