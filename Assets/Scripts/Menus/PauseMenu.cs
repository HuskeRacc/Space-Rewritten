using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public void OnClick_Settings()
    {
        MenuManager.OpenMenu(Menu.SETTINGS_MENU, gameObject);
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
