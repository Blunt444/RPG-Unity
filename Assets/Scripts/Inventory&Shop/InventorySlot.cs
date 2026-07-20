using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler
{
    public ItemSO itemSO;
    public int quantity;
    public TMP_Text quantityText;
    public Image itemImage;
    public RectTransform imageRectTransform;


    private InventoryManager inventoryManager;
    private static ShopManager activeShop;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
        Debug.Log(gameObject.name + " inventoryManager assigned: " + (inventoryManager != null));
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChange;
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChange;
    }

    private void HandleShopStateChange(ShopManager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("RAW POINTER DOWN on: " + gameObject.name);

        if (quantity > 0)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (activeShop != null)
                {
                    activeShop.SellItems(itemSO);
                    quantity--;
                    UpdateUI();
                }
                else
                {
                    inventoryManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                inventoryManager.DropItem(this);
            }
        }
        Debug.Log(itemSO);
    }

    public void UpdateUI()
    {
        if (quantity <= 0)
        {
            itemSO = null;
        }
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
