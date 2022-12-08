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
        this.gameObject.SetActive(false);
        PlayerMovement playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        if(playerMovement == null)
        {
            Debug.Log("failed to find playermovement");
            return;
        }
        playerMovement.Unpause();
    }

    public void OnClick_Exit()
    {
        SceneManager.LoadScene(0);
    }
}
