using System;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventorySlot[] inventorySlots;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;
    public GameObject lootPrefab;
    public Transform playerTransform;

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

    private void Start()
    {
        goldText.text = gold.ToString();
        foreach (InventorySlot slot in inventorySlots)
        {
            slot.UpdateUI();
        }
    }
    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemSO != null)
            {
                if (slot.itemSO.itemName.Equals(itemSO.itemName) && slot.quantity < itemSO.stackSize)
                {
                    int availableSpace = itemSO.stackSize - slot.quantity;
                    int amountToAdd = Mathf.Min(availableSpace, quantity);
                    slot.quantity += amountToAdd;
                    quantity -= amountToAdd;
                    slot.UpdateUI();
                    if (quantity <= 0)
                        return;
                }
            }
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemSO == null)
            {
                int amountToAdd = Mathf.Min(itemSO.stackSize, quantity);
                slot.itemSO = itemSO;
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;
                slot.UpdateUI();

                if (quantity <= 0)
                {
                    return;
                }
            }
        }
        if (quantity > 0)
        {
            DropLoot(itemSO, quantity);
        }
    }

    private void DropLoot(ItemSO itemSO, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, playerTransform.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSO, quantity);
    }

    public void DropItem(InventorySlot inventorySlot)
    {
        DropLoot(inventorySlot.itemSO, 1);
        inventorySlot.quantity--;
        if (inventorySlot.quantity <= 0)
        {
            inventorySlot.itemSO = null;
        }
        inventorySlot.UpdateUI();
    }

    public void UseItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.itemSO != null && inventorySlot.quantity > 0 && inventorySlot.itemSO.isUsable)
        {
            useItem.ApplyItemEffects(inventorySlot.itemSO);
            inventorySlot.quantity--;
            if (inventorySlot.quantity <= 0)
            {
                inventorySlot.itemSO = null;
            }
            inventorySlot.UpdateUI();
        }
    }

    public int GetItemCount(ItemSO itemSO)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemSO != null && slot.itemSO == itemSO)
            {
                return slot.quantity;
            }
        }

        return 0;
    }
    public void ReduceItemCount(ItemSO itemSO, int count)
    {
        int remaining = count;
        foreach (InventorySlot slot in inventorySlots)
        {
            if (remaining <= 0) break;

            if (slot.itemSO != null && slot.itemSO == itemSO)
            {
                int reduceAmount = Mathf.Min(slot.quantity, remaining);
                slot.quantity -= reduceAmount;
                remaining -= reduceAmount;

                if (slot.quantity <= 0) slot.itemSO = null;

                slot.UpdateUI();
            }
        }
    }

    public void PlayerDied(Vector3 pos)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.itemSO != null && slot.quantity > 0)
            {
                Loot loot = Instantiate(lootPrefab, pos, Quaternion.identity).GetComponent<Loot>();
                loot.Initialize(slot.itemSO, slot.quantity);
                slot.ResetSlot();
            }
        }
    }
}
