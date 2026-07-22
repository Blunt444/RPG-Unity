using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectSlot : MonoBehaviour
{
    public TMP_Text statAndDuration;
    public Image effectIcon;

    // private void Awake()
    // {
    //     statAndDuration = GetComponentInChildren<TMP_Text>();
    //     effectIcon = GetComponentInChildren<Image>();
    // }

    public void SetStatAndIcon(Sprite icon, string text)
    {
        effectIcon.sprite = icon;
        statAndDuration.text = text;
    }
}
