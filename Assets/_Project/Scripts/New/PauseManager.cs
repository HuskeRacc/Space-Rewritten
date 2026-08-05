using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;

    private bool isPaused = false;

    void Update()
    {
        // Check if the Escape key was pressed this frame
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        pauseMenu.SetActive(true);

        // Freeze game time
        Time.timeScale = 0f;

        // Disable camera look
        PlayerLook playerLook = FindAnyObjectByType<PlayerLook>();
        if (playerLook != null) playerLook.canLook = false;

        // Unlock the cursor so the player can click "Resume" or "Settings"
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Unpause()
    {
        isPaused = false;
        pauseMenu.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;

        // Re-enable camera look
        PlayerLook playerLook = FindAnyObjectByType<PlayerLook>();
        if (playerLook != null) playerLook.canLook = true;

        // Hide and lock the cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}