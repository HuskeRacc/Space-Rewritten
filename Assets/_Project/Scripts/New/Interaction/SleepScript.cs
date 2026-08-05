using UnityEngine;
using UnityEngine.InputSystem;

public class SleepScript : Interactable
{
    bool isSleeping = false;

    [SerializeField] GameObject sleepingPanel;
    [SerializeField] Camera sleepCamera;

    // We can remove the old GameObject crosshair reference and just use your DynamicCrosshair instance directly.

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if (PlayerNeeds.instance.fatigue < 80)
        {
            if (!isSleeping)
            {
                sleepCamera.gameObject.SetActive(true);
                sleepingPanel.SetActive(true);

                PlayerNeeds.instance.InvokeSleep();

                isSleeping = true;
                sleepCamera.depth = 2;
                Time.timeScale = 24.0f;

                // Hide the crosshair while sleeping using your singleton
                DynamicCrosshair.instance.SmoothCrosshairDisable();
            }
        }
        else
        {
            StartCoroutine(PlayerStatus.instance.TextPopup("Not tired enough.", 2, false));
        }
    }

    public override void OnLoseFocus()
    {
    }

    private void Update()
    {
        if (!isSleeping) return; // Ignore update if we aren't sleeping

        // Wake up if fatigue is full, OR if the player clicks the left mouse button to interrupt sleep
        if (PlayerNeeds.instance.fatigue >= 100 || Mouse.current.leftButton.wasPressedThisFrame)
        {
            WakeUp();
        }
    }

    private void WakeUp()
    {
        sleepingPanel.SetActive(false);
        sleepCamera.gameObject.SetActive(false);

        PlayerNeeds.instance.InvokeSleepBreak();

        sleepCamera.depth = 0;
        DynamicCrosshair.instance.SmoothCrosshairEnable();
        Time.timeScale = 1.0f;
        isSleeping = false;
    }
}