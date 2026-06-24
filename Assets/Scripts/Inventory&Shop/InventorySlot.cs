using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    public ItemSO itemSO;
    public int quantity;
    public TMP_Text quantityText;
    public Image itemImage;
    public RectTransform imageRectTransform;


    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                inventoryManager.UseItem(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
        // else if (quantity <= 0)
        // {
        //     itemSO = null;
        //     itemImage = null;
        //     quantityText.text = "";
        // }
        Debug.Log(itemSO);
    }

    public void UpdateUI()
    {
        if (itemSO == null)
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
            return;
        }

        itemImage.sprite = itemSO.icon;
        itemImage.gameObject.SetActive(true);
        quantityText.text = quantity.ToString();

        if (imageRectTransform == null)
        {
            imageRectTransform = itemImage.GetComponent<RectTransform>();
        }

        imageRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        imageRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        imageRectTransform.pivot = new Vector2(0.5f, 0.5f);

        imageRectTransform.anchoredPosition = itemSO.uiOffset;
        imageRectTransform.sizeDelta = new Vector2(200, 200);
        itemImage.preserveAspect = true;

        RectTransform textRect = quantityText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(1f, 0f);
        textRect.anchorMax = new Vector2(1f, 0f);
        textRect.pivot = new Vector2(1f, 0f);

        textRect.anchoredPosition = new Vector2(-10f, -5f);
    }

}
