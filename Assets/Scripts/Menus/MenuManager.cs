using UnityEngine;

public static class MenuManager
{
    public static bool IsInitialised { get; private set; }
    public static GameObject pauseMenu, settingsMenu, shopMenu, helmMenu, mainMenu;

    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        settingsMenu = canvas.transform.Find("SettingsMenu").gameObject;
        shopMenu = canvas.transform.Find("ShopMenu").gameObject;
        helmMenu = canvas.transform.Find("HelmMenu").gameObject;

        IsInitialised = true;
    }

    public static void OpenMenu(Menu menu, GameObject callingMenu)
    {
        if (!IsInitialised)
            Init();

        switch(menu)
        {
            case Menu.PAUSE_MENU:
                pauseMenu.SetActive(true);
                break;
            case Menu.SETTINGS_MENU:
                settingsMenu.SetActive(true);
                break;
            case Menu.SHOP_MENU:
                shopMenu.SetActive(true);
                break;
            case Menu.HELM_MENU:
                helmMenu.SetActive(true);
                break;
        }

        callingMenu.SetActive(false);
    }
}
