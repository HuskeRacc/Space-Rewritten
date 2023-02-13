using TMPro;
using UnityEngine;

public class SystemsInteract : Interactable
{
    [SerializeField] GameObject systemsMenu;
    [SerializeField] PlayerMovement player;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        systemsMenu.SetActive(true);
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
    }
}
