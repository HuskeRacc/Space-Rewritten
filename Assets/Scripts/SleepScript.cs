using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SleepScript : Interactable
{
    bool isSleeping = false;

    [SerializeField] GameObject sleepingPanel;

    [SerializeField] Camera sleepCamera;

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (PlayerNeeds.instance.fatigue < 80)
        {
            if (!isSleeping)
            {
                sleepingPanel.SetActive(true);
                PlayerNeeds.instance.InvokeSleep();
                isSleeping = true;
                sleepCamera.depth = 2;
                Time.timeScale = 24.0f;
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
        if(PlayerNeeds.instance.fatigue >= 100 && isSleeping)
        {
            sleepingPanel.SetActive(false);
            PlayerNeeds.instance.InvokeSleepBreak();
            sleepCamera.depth = 0;
            DynamicCrosshair.instance.SmoothCrosshairEnable();
            Time.timeScale = 1.0f;

            isSleeping = false;
        }
    }
}
