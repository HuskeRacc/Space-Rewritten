using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] PlayerStatus status;
    [SerializeField] ShipSystems ship;

    [SerializeField] GameObject upgradesMenu;
    [SerializeField] GameObject shopMenu;

    public void OnClick_Back()
    {
        shopMenu.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnClick_UpgradesMenu()
    {
        upgradesMenu.SetActive(true);
        shopMenu.SetActive(false);
    }

    public void OnClick_BuyFuel()
    {
        if (ShipMaterialBank.instance.fueliumBanked >= ShopPrices.instance.fuelPrice)
        {
            ShipMaterialBank.instance.fueliumBanked -= ShopPrices.instance.fuelPrice;
            ItemSpawner.instance.SpawnSmallItem(3);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough fuelium!", 2, false));
        }
    }

    public void OnClick_BuyMRE()
    {
        if(ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.mrePrice)
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.mrePrice;
            ItemSpawner.instance.SpawnSmallItem(0);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough satonium!", 2, false));
        }
    }

    public void OnClick_BuyDonut()
    {
        if (ShipMaterialBank.instance.satoniumBanked >= ShopPrices.instance.donutPrice)
        {
            ShipMaterialBank.instance.satoniumBanked -= ShopPrices.instance.donutPrice;
            ItemSpawner.instance.SpawnSmallItem(1);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough satonium!", 2, false));
        }
    }

    public void OnClick_BuyBattery()
    {
        if (ShipMaterialBank.instance.fueliumBanked >= ShopPrices.instance.batteryPrice)
        {
            ShipMaterialBank.instance.fueliumBanked -= ShopPrices.instance.batteryPrice;
            ItemSpawner.instance.SpawnSmallItem(2);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough fuelium!", 2, false));
        }
    }


}
