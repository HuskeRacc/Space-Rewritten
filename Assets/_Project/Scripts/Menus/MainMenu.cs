using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private string SavePath => $"{Application.persistentDataPath}/savedata.txt";
    public void OnClick_NewGame()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
        }
        SceneManager.LoadScene(1);
    }

    public void OnClick_ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClick_Quit()
    {
        Application.Quit();
    }
}
