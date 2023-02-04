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
        player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
    }


    public void OnClick_Back()
    {
        helmMenu.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
