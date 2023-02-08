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
        if(!GameObject.Find("Player").TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            return;
        }
        playerMovement.Unpause();
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene(0);
    }
}
