using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] PlayerStatus status;
    [SerializeField] ShipSystems ship;


    public void OnClick_Back()
    {
       this.gameObject.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnClick_BuyFuel()
    {
        if (playerCurrency.currency >= ShopPrices.instance.fuelPrice)
        {
            ship.fuel += 75f;
            playerCurrency.currency -= ShopPrices.instance.fuelPrice;
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough Money!", 2));
        }
    }

    public void OnClick_BuyMRE()
    {
        if(playerCurrency.currency >= ShopPrices.instance.mrePrice)
        {
            playerCurrency.currency -= ShopPrices.instance.mrePrice;
            ItemSpawner.instance.SpawnSmallItem(0);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough Money!", 2));
        }
    }

    public void OnClick_BuyDonut()
    {
        if (playerCurrency.currency >= ShopPrices.instance.donutPrice)
        {
            playerCurrency.currency -= ShopPrices.instance.donutPrice;
            ItemSpawner.instance.SpawnSmallItem(1);
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough Money!", 2));
        }
    }

}
