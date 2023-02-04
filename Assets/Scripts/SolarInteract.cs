using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarInteract : Interactable
{
    [SerializeField] GameObject solarMenu;
    [SerializeField] PlayerMovement player;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        solarMenu.SetActive(true);
        player.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
        
    }

    public void OnClick_CloseMenu()
    {
        solarMenu.SetActive(false);
        player.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
