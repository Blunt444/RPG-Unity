using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    public GameObject shopSlotPrefab;
    public Transform shopBox;
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
        foreach (ShopSlot slot in shopSlots)
        {
            if (slot == null) continue;

            Destroy(slot.gameObject);
        }

        shopSlots.Clear();

        for (int i = 0; i < shopItems.Count; i++)
        {
            if (shopItems[i] == null || shopItems[i].itemSO == null) continue;

            ShopItems shopItem = shopItems[i];
            GameObject slot = Instantiate(shopSlotPrefab, shopBox);
            ShopSlot script = slot.GetComponent<ShopSlot>();

            script.Initialize(shopItem.itemSO, shopItem.price);

            shopSlots.Add(script);
        }

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

    public void TryBuyItem(ItemSO itemSO, int price)
    {
        if (itemSO == null || inventoryManager.gold < price) return;
        if (HasSpaceForItem(itemSO))
        {
            inventoryManager.gold -= price;
            inventoryManager.goldText.text = inventoryManager.gold.ToString();
            inventoryManager.AddItem(itemSO, 1);
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
