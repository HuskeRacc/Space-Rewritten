using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] PlayerStatus status;
    [SerializeField] ShopScreen shop;
    [SerializeField] ShipSystems ship;

    public void OnClick_Back()
    {
       this.gameObject.SetActive(false);
        player.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnClick_BuyFuel()
    {
        if (playerCurrency.currency >= shop.fuelPrice)
        {
            ship.fuel += 75f;
            playerCurrency.currency -= shop.fuelPrice;
        }
        else
        {
            StartCoroutine(status.TextPopup("Not Enough Money!", 2));
        }
         
    }
}
