using System;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public ItemSO itemSO;
    public SpriteRenderer sr;
    public Animator anim;
    public int quantity;
    public bool canBePickedup = true;
    public string id;
    public static event Action<ItemSO, int> OnItemLooted;

    private void Start()
    {
        if (id == null || id == "")
        {
            id = Id.CreateId(transform.position);
        }
        if (InventoryManager.Instance.loots.ContainsKey(id))
        {
            if (InventoryManager.Instance.loots[id].isDestroyed)
            {
                Destroy(gameObject);
            }
        }
        else
            InventoryManager.Instance.loots[id] = new LootInfo { isDestroyed = false, pos = transform.position };
    }


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
            SetIsDestroyed();
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
            SetIsDestroyed();
            Destroy(gameObject, 0.5f);
        }
    }

    private void SetIsDestroyed()
    {
        InventoryManager.Instance.loots[id].isDestroyed = true;
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

        id = Id.CreateId(transform.position);

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

[Serializable]
public class LootInfo
{
    public bool isDestroyed;
    public Vector3 pos;
}
