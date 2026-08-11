using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuestBox : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text questLabel;
    public TMP_Text questDesc;
    public Image fillImage;
    public Action<QuestSO> onClick;

    private QuestSO questSO;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClick != null)
        {
            onClick?.Invoke(questSO);
        }
    }

    public void Setup(QuestSO questSO, Action<QuestSO> onClick)
    {
        questLabel.text = TruncateAndEllipse(questSO.label, 30);
        questDesc.text = TruncateAndEllipse(questSO.about, 100);
        fillImage.fillAmount = questSO.Progress();
        this.questSO = questSO;

        this.onClick = onClick;
    }

    private string TruncateAndEllipse(string text, int letters)
    {
        int length = text.Length;
        int reduceBy = length > letters ? letters : length;
        return text.Substring(0, reduceBy - 3) + "...";
    }
}
