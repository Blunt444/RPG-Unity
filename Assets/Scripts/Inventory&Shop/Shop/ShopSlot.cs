using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemSO itemSO;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;
    public RectTransform imageRectTransform;
    public int price;

    [SerializeField] private ShopInfo shopInfo;

    private void Start()
    {
        shopInfo = GameObject.FindGameObjectWithTag("ShopInfoPanel").GetComponent<ShopInfo>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSO == null) return;
        shopInfo.ShowItemInfo(itemSO);
        Color color = itemImage.color;
        color.a = 0.5f;
        itemImage.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemSO == null) return;
        shopInfo.HideItemInfo();
        Color color = itemImage.color;
        color.a = 1f;
        itemImage.color = color;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (itemSO == null) return;
        shopInfo.FollowMouse();
    }

    public void Initialize(ItemSO newItemSO, int price)
    {
        // Debug.Log(newItemSO);
        itemSO = newItemSO;
        itemImage.sprite = itemSO.icon;
        itemNameText.text = itemSO.itemName;
        this.price = price;
        priceText.text = price.ToString();

        if (imageRectTransform == null)
        {
            imageRectTransform = itemImage.GetComponent<RectTransform>();
        }

        imageRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        imageRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        imageRectTransform.pivot = new Vector2(0.5f, 0.5f);

        Vector2 topPadding = new Vector2(0, -10f);
        imageRectTransform.anchoredPosition = itemSO.shopUIOffset + topPadding;

        imageRectTransform.sizeDelta = itemSO.shopUIItemSize;

        itemImage.preserveAspect = true;

    }

    public void OnBuyButtonClicked()
    {
        Debug.Log("shop item");
        ShopManager.Instance.TryBuyItem(itemSO, price, this);
    }

}
