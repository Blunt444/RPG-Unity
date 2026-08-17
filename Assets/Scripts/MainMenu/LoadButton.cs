using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text textBox;
    public string text;
    public LoadButtonAction action;
    public string fileName;
    public static event Action<LoadButtonAction, string> LoadButtonClicked;

    public void OnPointerDown(PointerEventData eventData)
    {
        LoadButtonClicked?.Invoke(action,fileName);
        textBox.text = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textBox.text = $"<u>{text}</u>";
        textBox.alpha = 0.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.text = text;
        textBox.alpha = 1f;
    }
}

public enum LoadButtonAction
{
    Load,
    Delete
}