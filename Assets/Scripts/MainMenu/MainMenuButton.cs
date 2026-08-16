using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    public ButtonAction action;
    public void OnPointerDown(PointerEventData eventData)
    {
        MainMenu.Instance.ButtonClicked(this);
        text.alpha = 1f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.alpha = 0.5f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.alpha = 1f;
    }
}
