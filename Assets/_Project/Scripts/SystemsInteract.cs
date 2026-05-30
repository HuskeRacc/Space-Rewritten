using UnityEngine;

public class SystemsInteract : Interactable
{
    [SerializeField] GameObject systemsMenu;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        systemsMenu.SetActive(true);
        PlayerMovement.instance.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnLoseFocus()
    {
    }
}
