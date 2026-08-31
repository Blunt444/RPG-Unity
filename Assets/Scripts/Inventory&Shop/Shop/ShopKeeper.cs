using UnityEngine;
using System.Collections.Generic;
using System;
public class ShopKeeper : MonoBehaviour
{

    public static ShopKeeper currentShopKeeper;

    public Animator anim;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;

    [SerializeField] private List<ShopItems> shopEatables;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private Camera shopKeeperCam;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -1);
    public static event Action<ShopManager, bool> OnShopStateChanged;


    private bool playerInRange;
    private bool isShopOpen;

    void Update()
    {
        if (playerInRange)
        {
            if (isShopOpen)
            {
                if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Interact"))
                {
                    currentShopKeeper = null;
                    Time.timeScale = 1;
                    shopCanvasGroup.alpha = 0;
                    shopCanvasGroup.interactable = false;
                    shopCanvasGroup.blocksRaycasts = false;
                    isShopOpen = false;

                    shopKeeperCam.gameObject.SetActive(false);
                }
            }
            else
            {
                if (Input.GetButtonDown("Interact"))
                {
                    currentShopKeeper = this;
                    Time.timeScale = 0;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;
                    isShopOpen = true;

                    shopKeeperCam.transform.position = transform.position + cameraOffset;
                    shopKeeperCam.gameObject.SetActive(true);

                    OpenItemShop();
                }
            }
        }
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopItems(shopWeapons);
    }

    public void OpenEatablesShop()
    {
        shopManager.PopulateShopItems(shopEatables);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            anim.SetBool("PlayerInRange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            anim.SetBool("PlayerInRange", false);
        }
    }

}
