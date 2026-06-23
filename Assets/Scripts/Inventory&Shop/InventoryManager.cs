using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public UseItem useItem;
    public int gold;
    public TMP_Text goldText;

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
        else
        {
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }

            }
        }
    }

    public void UseItem(InventorySlot inventorySlot)
    {
        if (inventorySlot.itemSO != null && inventorySlot.quantity > 0)
        {
            useItem.ApplyItemEffects(inventorySlot.itemSO);
            inventorySlot.quantity--;
            if (inventorySlot.quantity <= 0)
            {
                inventorySlot.itemSO = null;
            }
        }
    }
}
