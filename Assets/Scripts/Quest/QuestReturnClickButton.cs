using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestRetrunClickButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text box;
    public string text;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnReturnClicked();
        box.text = text;
    }

    public void OnReturnClicked()
    {
        QuestDisplay questDisplay = QuestDisplay.Instance;
        if (questDisplay == null || questDisplay.questContainer == null) return;

        questDisplay.questContainer.gameObject.SetActive(true);
        questDisplay.questInfoContainer.gameObject.SetActive(false);

        questDisplay.type = QuestPanelOpenType.Quests;

        questDisplay.Display();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        box.text = $"<u>{text}</u>";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        box.text = text;
    }
}
