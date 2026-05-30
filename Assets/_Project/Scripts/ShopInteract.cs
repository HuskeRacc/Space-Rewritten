using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{
    [SerializeField] GameObject shopMenu;
    [SerializeField] PlayerMovement player;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        shopMenu.SetActive(true);
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {

    }

    public void OnClick_CloseMenu()
    {
        shopMenu.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
