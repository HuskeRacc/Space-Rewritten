using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SleepScript : Interactable
{

    [SerializeField] private InputActionReference interactAction;

    bool isSleeping = false;

    [SerializeField] GameObject sleepingPanel;

    [SerializeField] Camera sleepCamera;

    [SerializeField] GameObject crosshair;

    private void OnEnable()
    {
        interactAction?.action.Enable();
    }

    private void OnDisable()
    {
        interactAction?.action.Disable();
    }

    public override void OnFocus()
    {
        
    }

    public override void OnInteract()
    {
        if (PlayerNeeds.instance.fatigue < 80)
        {
            if (!isSleeping)
            {
                PlayerMovement.instance.canPause = false;
                sleepingPanel.SetActive(true);
                PlayerNeeds.instance.InvokeSleep();
                isSleeping = true;
                sleepCamera.depth = 2;
                Time.timeScale = 24.0f;
                crosshair.SetActive(false);
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
            crosshair.SetActive(true);
            Time.timeScale = 1.0f;
            isSleeping = false;
            PlayerMovement.instance.canPause = true;
        }

        if(interactAction.action.WasPressedThisFrame() && isSleeping)
        {
            sleepingPanel.SetActive(false);
            PlayerNeeds.instance.InvokeSleepBreak();
            sleepCamera.depth = 0;
            crosshair.SetActive(true);
            Time.timeScale = 1.0f;
            isSleeping = false;
            PlayerMovement.instance.canPause = true;
        }
    }
}
