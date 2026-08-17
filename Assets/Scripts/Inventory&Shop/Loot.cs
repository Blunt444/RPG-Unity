using System;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;
    public int quantity;
    public bool canBePickedup = true;
    public static event Action<ItemSO, int> OnItemLooted;

    private void OnValidate()
    {
        if (itemSO == null) return;
        UpdateAppearnace();
    }

    private void EnablePickUp()
    {
        canBePickedup = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedup)
        {
            canBePickedup = false;

            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity);
            Destroy(gameObject, 0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && canBePickedup)
        {
            canBePickedup = false;

            anim.Play("LootPickup");
            OnItemLooted?.Invoke(itemSO, quantity);
            Destroy(gameObject, 0.5f);
        }
    }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
    //         canBePickedup = true;
    //     }
    // }

    private void UpdateAppearnace()
    {
        sr.sprite = itemSO.icon;
        this.name = itemSO.itemName;

    }
    public void Initialize(ItemSO itemSO, int quantity)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedup = false;
        UpdateAppearnace();

        Invoke(nameof(EnablePickUp), 0.25f);
    }

    public void Initialize(ItemSO itemSO, int quantity, float waitTime)
    {
        this.itemSO = itemSO;
        this.quantity = quantity;
        canBePickedup = false;
        UpdateAppearnace();

        Invoke(nameof(EnablePickUp), waitTime);
    }

    public void DropWoodAnimation()
    {
        anim.Play("DropWood");
    }
}
