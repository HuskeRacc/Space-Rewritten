using UnityEngine;

public class HelmInteract : Interactable
{
    [SerializeField] GameObject helmMenu;
    [SerializeField] PlayerMovement player;

    public override void OnFocus()
    {

    }

    public override void OnInteract()
    {
        helmMenu.SetActive(true);
        player.CanMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {

    }
}
