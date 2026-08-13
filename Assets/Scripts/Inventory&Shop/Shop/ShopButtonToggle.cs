using UnityEngine;

public class ShopButtonToggle : MonoBehaviour
{
    public static ShopButtonToggle Instance;

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

    public void ClickedShopButton(string type)
    {
        if (type == "items")
        {
            OpenItemShop();
        }
        else if (type == "weapons")
        {
            OpenWeaponShop();
        }
        else
        {
            OpenEatablesShop();
        }
    }

    public void OpenItemShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenItemShop();
    }

    public void OpenWeaponShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenWeaponShop();
    }

    public void OpenEatablesShop()
    {
        if (ShopKeeper.currentShopKeeper == null) return;
        ShopKeeper.currentShopKeeper.OpenEatablesShop();
    }
}
