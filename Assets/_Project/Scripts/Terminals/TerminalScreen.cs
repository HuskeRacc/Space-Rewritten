using UnityEngine;

public class TerminalScreen : Interactable
{
    [SerializeField] private TerminalType terminalType;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        TerminalUIManager.instance.OpenTerminal(terminalType);
    }

    public override void OnLoseFocus()
    {
    }
}
