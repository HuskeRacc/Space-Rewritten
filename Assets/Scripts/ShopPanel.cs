using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Interactable
{
    [SerializeField] GameObject shopMenu;
    [SerializeField] PlayerMovement player;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        shopMenu.SetActive(true);
        player.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {

    }

    public void OnClick_CloseMenu()
    {
        shopMenu.SetActive(false);
        player.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
