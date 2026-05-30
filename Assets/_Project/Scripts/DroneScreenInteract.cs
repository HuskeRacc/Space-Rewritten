using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneScreenInteract : Interactable
{
    [SerializeField] GameObject droneRepairsMenu;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        droneRepairsMenu.SetActive(true);
        PlayerMovement.instance.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
        
    }
}
