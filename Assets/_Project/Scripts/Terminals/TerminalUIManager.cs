using UnityEngine;

public class TerminalUIManager : MonoBehaviour
{
    public static TerminalUIManager instance;

    [Header("Panels")]
    [SerializeField] GameObject shopMenu;
    [SerializeField] GameObject solarMenu;
    [SerializeField] GameObject systemsMenu;
    [SerializeField] GameObject helmMenu;
    [SerializeField] GameObject repairMenu;

    [Header("Player")]
    [SerializeField] PlayerMovement player;

    GameObject currentMenu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CloseAllMenus();
    }

    public void OpenTerminal(TerminalType terminalType)
    {
        CloseAllMenus();

        currentMenu = GetMenuForType(terminalType);

        if (currentMenu != null)
        {
            currentMenu.SetActive(true);
        }

        SetPlayerMenuMode(true);
    }

    private GameObject GetMenuForType(TerminalType terminalType)
    {
        switch(terminalType)
        {
            case TerminalType.Shop:
                return shopMenu;

            case TerminalType.Solar:
                return solarMenu;

            case TerminalType.Systems:
                return systemsMenu;

            case TerminalType.Drone:
                return helmMenu;

            case TerminalType.Repair:
                return repairMenu;

            default:
                Debug.LogWarning("No menu assigned for terminal type: " + terminalType);
                return null;
        }
    }

    private void CloseAllMenus()
    {
        if (shopMenu  != null) shopMenu.SetActive(false);
        if (solarMenu != null) solarMenu.SetActive(false);
        if (systemsMenu != null) systemsMenu.SetActive(false);
        if (helmMenu != null) helmMenu.SetActive(false);
        if (repairMenu != null) repairMenu.SetActive(false);

        currentMenu = null;
    }

    public void CloseCurrentTerminal()
    {
        if (currentMenu != null)
        {
            currentMenu.SetActive(false);
            currentMenu = null;
        }
        SetPlayerMenuMode(false);
    }

    private void SetPlayerMenuMode(bool menuOpen)
    {
        if (player == null)
        {
            player = PlayerMovement.instance;
        }

        if (player != null)
        {
            player.canMove = !menuOpen;
        }

        Cursor.lockState = menuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = menuOpen;
    }
}
