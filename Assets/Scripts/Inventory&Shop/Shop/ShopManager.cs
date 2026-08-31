using UnityEngine;
using System.Collections.Generic;
using System;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public GameObject shopSlotPrefab;
    public Transform shopBox;
    public Transform emptyMessage;
    public int messageTimer = 4;
    public static event Action<string, int> Message;

    [SerializeField] private List<ShopSlot> shopSlots = new List<ShopSlot>();

    [SerializeField] private InventoryManager inventoryManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PopulateShopItems(List<ShopItems> shopItems)
    {

        ClearShop();

        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i] == null || shopItems[i].itemSO == null) continue;

            ShopItems shopItem = shopItems[i];
            GameObject slot = Instantiate(shopSlotPrefab, shopBox);
            ShopSlot script = slot.GetComponent<ShopSlot>();

            script.Initialize(shopItem.itemSO, shopItem.price);

            shopSlots.Add(script);
        }

        emptyMessage.gameObject.SetActive(shopSlots.Count <= 0);

    }

    public void ClearShop()
    {
        foreach (ShopSlot slot in shopSlots)
        {
            if (slot == null) continue;

            Destroy(slot.gameObject);
        }
        shopSlots.Clear();
    }

    public void SellItems(ItemSO itemSO)
    {
        if (itemSO == null) return;

        foreach (ShopSlot slot in shopSlots)
        {
            if (slot.itemSO == itemSO)
            {
                inventoryManager.gold += slot.price / 2;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }

    public void TryBuyItem(ItemSO itemSO, int price, ShopSlot slot)
    {
        Debug.Log("shop item buy");

        if (itemSO == null)
        {
            Message?.Invoke("Invalid Item.", messageTimer);
            return;
        }

        if (inventoryManager.gold < price)
        {
            Message?.Invoke("Not enough Money.", messageTimer);
            return;
        }

        if (itemSO.isArrow)
        {
            Debug.Log("arrow");
            bool bought = ArrowQuantityManager.Instance.SetQuantity(1);
            if (bought)
                inventoryManager.gold -= price;
            else
                Message?.Invoke("Maximum Capacity Reached", messageTimer);
            inventoryManager.goldText.text = inventoryManager.gold.ToString();
            return;
        }

        if (HasSpaceForItem(itemSO))
        {
            inventoryManager.gold -= price;
            inventoryManager.goldText.text = inventoryManager.gold.ToString();
            inventoryManager.AddItem(itemSO, 1);
        }
        else
        {
            Message?.Invoke("Not Enough Space", messageTimer);
        }
    }

    private bool HasSpaceForItem(ItemSO itemSO)
    {
        foreach (InventorySlot slot in inventoryManager.inventorySlots)
        {
            if (slot.itemSO == itemSO && slot.quantity < itemSO.stackSize) return true;
            else if (slot.itemSO == null) return true;
        }
        return false;
    }
}

[System.Serializable]
public class ShopItems
{
    public ItemSO itemSO;
    public int price;
}
