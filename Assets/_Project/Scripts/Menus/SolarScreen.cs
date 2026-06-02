using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SolarScreen : MonoBehaviour
{
    [SerializeField] GameObject solarMenu;
    [SerializeField] TextMeshProUGUI efficiencyValue;
    [SerializeField] TextMeshProUGUI batteryValue;
    [SerializeField] TextMeshProUGUI chargeRateValue;

    [SerializeField] AudioSource solarPanelAudioSource;
    [SerializeField] AudioClip solarPanelCleanAudioClip;

    private void Update()
    {
        efficiencyValue.text = ShipSystems.instance.solarEfficiency.ToString("F0");
        batteryValue.text = ShipSystems.instance.shipBattery.ToString("F0");
        chargeRateValue.text = ShipSystems.instance.amountToAdd.ToString("F1");
    }


    public void OnClick_Back()
    {
        solarMenu.SetActive(false);
        PlayerMovement.instance.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnClick_CleanSolars()
    {
        solarPanelAudioSource.PlayOneShot(solarPanelCleanAudioClip);
        ShipSystems.instance.solarEfficiency = 100f;
    }

    public void OnClick_Enable()
    {
        ShipSystems.instance.Enable();
    }

    public void OnCLick_Disable()
    {
        ShipSystems.instance.Disable();

    }
}
