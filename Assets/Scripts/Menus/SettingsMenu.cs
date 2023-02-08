using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;

    public void OnClick_Back()
    {
        this.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
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
