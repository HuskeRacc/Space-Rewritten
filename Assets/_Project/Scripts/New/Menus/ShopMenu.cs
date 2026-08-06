using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] PlayerStatus status;
    [SerializeField] ShipSystems ship;

    [SerializeField] GameObject upgradesMenu;
    [SerializeField] GameObject shopMenu;

    [Header("Item Stations")]
    [SerializeField] ItemStation foodStation;
    [SerializeField] ItemStation batteryStation;
    [SerializeField] ItemStation coffeeStation;

    public void OnClick_Back()
    {
        shopMenu.SetActive(false);

        PlayerLook playerLook = FindAnyObjectByType<PlayerLook>();
        if (playerLook != null) playerLook.canLook = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnClick_BuyFood()
    {
        if (foodStation != null && foodStation.currentUses >= foodStation.maxCapacity)
        {
            StartCoroutine(status.TextPopup("Food station is full!", false));
            return;
        }

        if (ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.mrePrice)
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.mrePrice;
            foodStation.AddStock(1);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough satonium!", false));
        }
    }

    public void OnClick_BuyBattery()
    {
        if (batteryStation != null && batteryStation.currentUses >= batteryStation.maxCapacity)
        {
            StartCoroutine(status.TextPopup("Battery station is full!", false));
            return;
        }

        if (ShipMaterialBank.instance.fueliumBanked >= ShopPrices.instance.batteryPrice)
        {
            ShipMaterialBank.instance.fueliumBanked -= ShopPrices.instance.batteryPrice;
            batteryStation.AddStock(1);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough fuelium!", false));
        }
    }

    public void OnClick_BuyCoffee()
    {
        if (coffeeStation != null && coffeeStation.currentUses >= coffeeStation.maxCapacity)
        {
            StartCoroutine(status.TextPopup("Coffee station is full!", false));
            return;
        }

        if (ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.coffeePrice)
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.coffeePrice;
            coffeeStation.AddStock(1);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough satonium!", false));
        }
    }
}