using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{

    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI volumeValue;
    [SerializeField] TextMeshProUGUI sensitivityValue;

    

    private void Update()
    {
        volumeValue.text = PlayerPrefs.GetFloat("volume").ToString("F0");
        sensitivityValue.text = PlayerPrefs.GetFloat("sensitivity").ToString("F0");
    }

    public void OnClick_Back()
    {
        this.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void OnClick_MenuBack()
    {
        this.gameObject.SetActive(false);
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
