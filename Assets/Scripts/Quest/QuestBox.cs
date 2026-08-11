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
        questLabel.text = TruncateAndEllipse(questSO.label, 60);
        questDesc.text = TruncateAndEllipse(questSO.about, 200);
        fillImage.fillAmount = questSO.Progress();
        this.questSO = questSO;

        this.onClick = onClick;
    }

    private string TruncateAndEllipse(string text, int letters)
    {
        if (text.Length <= letters)
            return text;
        int reduceby = Math.Max(letters - 3, 0);
        return text.Substring(0, reduceby) + "...";
    }
}
