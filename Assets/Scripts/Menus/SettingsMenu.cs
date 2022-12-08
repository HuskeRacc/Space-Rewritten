using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void OnClick_Back()
    {
        MenuManager.OpenMenu(Menu.PAUSE_MENU, gameObject);
    }

    public void OnClick_BackMainMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene(0);
    }
}
