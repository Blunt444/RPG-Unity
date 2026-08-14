using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;

    [Header("Stat Field")]
    public TMP_Text[] statsText;

    private RectTransform infoPanelRect;

    private void Awake()
    {
        infoPanelRect = gameObject.GetComponent<RectTransform>();
    }

    public void ShowItemInfo(ItemSO itemSO)
    {

        CleanUpStatBlock();

        infoPanel.alpha = 1;
        // infoPanel.interactable = true;
        // infoPanel.blocksRaycasts = true;

        itemNameText.text = itemSO.itemName;
        itemDescriptionText.text = itemSO.itemDescription;

        List<string> stats = new List<string>();
        if (itemSO.currentHealth > 0) stats.Add("Health: " + itemSO.currentHealth);
        if (itemSO.damage > 0) stats.Add("Damage: " + itemSO.damage);
        if (itemSO.speed > 0) stats.Add("Speed: " + itemSO.speed);
        if (itemSO.duration > 0) stats.Add("Dur: " + itemSO.duration + 'S');

        if (stats.Count <= 0) return;

        for (int i = 0; i < statsText.Length; i++)
        {
            if (i < stats.Count)
            {
                statsText[i].text = stats[i];
                statsText[i].gameObject.SetActive(true);
            }
            else
            {
                statsText[i].gameObject.SetActive(false);
            }
        }
    }

    private void CleanUpStatBlock()
    {
        for (int i = 0; i < statsText.Length; i++)
        {
            statsText[i].gameObject.SetActive(false);
        }
    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;

        itemNameText.text = "";
        itemDescriptionText.text = "";
    }

    public void FollowMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);

        infoPanelRect.position = mousePosition + offset;
    }

}
