using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // public string text = "";
    public string type = "";
    public Image image;
    // public TMP_Text label;

    // public void Start()
    // {
    //     label.text = text;
    // }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShopButtonToggle.Instance.ClickedShopButton(type);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = image.color;
        color.a = 0.5f;
        image.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }
}
