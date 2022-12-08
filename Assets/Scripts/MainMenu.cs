using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;

    public void OnClick_Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_Settings()
    {
        settingsMenu.SetActive(true);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
