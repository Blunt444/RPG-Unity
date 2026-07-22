using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItemInfoManager : MonoBehaviour
{

    [Serializable]
    public struct SpriteEntry
    {
        public string key;
        public Sprite icon;
    }

    public static SlotItemInfoManager Instance;
    public TMP_Text itemDesc;
    public RectTransform effectPanel;
    public GameObject effectSlotPrefab;
    public Camera uiCamera;
    public RectTransform mainPanel;
    public RectTransform rootCanvas;

    [SerializeField]
    private List<SpriteEntry> spriteList = new List<SpriteEntry>();
    private Dictionary<string, Sprite> map = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            foreach (SpriteEntry spriteEntry in spriteList)
            {
                map[spriteEntry.key] = spriteEntry.icon;
            }
            ClearEffectSlotsAndDesc();
            SetInfoPanelVisibleState(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPanelPos(Vector2 mouseScreenPos)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootCanvas,
            mouseScreenPos,
            uiCamera,
            out localPoint
        );

        float halfWidth = (mainPanel.rect.width * mainPanel.localScale.x) / 2f;
        float halfHeight = (mainPanel.rect.height * mainPanel.localScale.y) / 2f;
        float gap = 10f;

        bool rightHasRoom = (mouseScreenPos.x + gap + mainPanel.rect.width) <= Screen.width;

        float xOffset = rightHasRoom ? (halfWidth + gap) : -(halfWidth + gap);

        Vector2 offset = new Vector2(xOffset, -(halfHeight + 10f));

        mainPanel.anchoredPosition = localPoint + offset;
    }

    public void SetInfoPanelVisibleState(bool isVisible)
    {
        Instance.gameObject.SetActive(isVisible);
    }

    public void SetItemDesc(string desc)
    {
        itemDesc.text = desc;
    }

    public void CreateEffectSlots(ItemSO itemSO)
    {
        if (itemSO.currentHealth > 0)
        {
            var slot = Instantiate(effectSlotPrefab, effectPanel);
            EffectSlot effectSlotScript = slot.GetComponent<EffectSlot>();

            string stat = "" + itemSO.currentHealth + (itemSO.duration > 0 ? ", " + itemSO.duration : "");

            effectSlotScript.SetStatAndIcon(map["currentHealth"], stat);
        }
        if (itemSO.maxHealth > 0)
        {
            var slot = Instantiate(effectSlotPrefab, effectPanel);
            EffectSlot effectSlotScript = slot.GetComponent<EffectSlot>();

            string stat = "" + itemSO.maxHealth + (itemSO.duration > 0 ? ", " + itemSO.duration : "");

            effectSlotScript.SetStatAndIcon(map["maxHealth"], stat);
        }
        if (itemSO.speed > 0)
        {
            var slot = Instantiate(effectSlotPrefab, effectPanel);
            EffectSlot effectSlotScript = slot.GetComponent<EffectSlot>();

            string stat = "" + itemSO.speed + (itemSO.duration > 0 ? ", " + itemSO.duration : "");

            effectSlotScript.SetStatAndIcon(map["speed"], stat);
        }
        if (itemSO.damage > 0)
        {
            var slot = Instantiate(effectSlotPrefab, effectPanel);
            EffectSlot effectSlotScript = slot.GetComponent<EffectSlot>();

            string stat = "" + itemSO.damage + (itemSO.duration > 0 ? ", " + itemSO.duration : "");

            effectSlotScript.SetStatAndIcon(map["damage"], stat);
        }
    }

    public void ClearEffectSlotsAndDesc()
    {
        itemDesc.text = "";
        foreach (Transform child in effectPanel)
        {
            Destroy(child.gameObject);
        }

        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(effectPanel);

        Debug.Log("EffectPanel height after clear: " + effectPanel.rect.height);
    }

}
