using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject settingsMenu;

    public void OnClick_Settings()
    {
        settingsMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnClick_Resume()
    {
        PauseManager pauseManager = FindAnyObjectByType<PauseManager>();
        if (pauseManager != null)
        {
            pauseManager.Unpause();
        }
    }

    public void OnClick_Exit()
    {
        SavingLoading.instance.OnClick_Save();
        SceneManager.LoadScene(0);
    }
}
