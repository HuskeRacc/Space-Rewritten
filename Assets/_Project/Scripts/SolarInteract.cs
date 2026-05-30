using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarInteract : Interactable
{
    [SerializeField] GameObject solarMenu;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        solarMenu.SetActive(true);
        PlayerMovement.instance.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
        
    }

}
