using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerGenerator : Interactable
{
    public bool powerGeneratorActive;
    public bool powerGeneratorAvailable;

    [SerializeField] AudioSource powerSound;
    [SerializeField] OxygenGenerator oxygen;
    [SerializeField] ShipSystems ship;
    [SerializeField] PlayerStatus status;

    [SerializeField] float fuelConsumptionRate = 1f;
    [SerializeField] float fuelConsumptionTickRate = 5f;

    [Header("Displays")]
    [SerializeField] TextMeshProUGUI statusDisplayTXT;

    private void Start()
    {
        StartCoroutine(ConsumeFuel());
    }

    private void Update()
    {
        FuelCheck();
        HandlePower();

        if(powerGeneratorActive)
        {
            statusDisplayTXT.text = "Running.";
            statusDisplayTXT.color = Color.green;
        }
        else
        {
            statusDisplayTXT.text = "Inactive.";
            statusDisplayTXT.color = Color.red;
        }
    }

    void FuelCheck()
    {
        if (ship.fuel > 100)
        {
            ship.fuel = 100;
        }

        if(ship.fuel <= 0)
        {
            ship.fuel = 0;
            powerGeneratorActive = false;
            powerGeneratorAvailable = false;
        }
        else
        {
            powerGeneratorAvailable = true;
        }

    }

    public void HandlePower()
    {
        if (powerGeneratorActive)
        {
            powerSound.UnPause();
        }
        if (!powerGeneratorActive)
        {
            powerSound.Pause();
        }

        if (powerGeneratorAvailable)
        {
            if (powerGeneratorActive)
            {
                oxygen.o2GeneratorAvailable = true;
            }
            else
            {
                oxygen.o2GeneratorActive = false;
                oxygen.o2GeneratorAvailable = false;
            }
        }
    }

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if(ship.fuel > 0)
        {
            TogglePowerGenerator();
        }
        else
        {
            StartCoroutine(status.TextPopup("No Fuel.", 5));
        }
    }

    public override void OnLoseFocus()
    {
    }

    public void TogglePowerGenerator()
    {
        if (powerGeneratorAvailable)
        {
            powerGeneratorActive = !powerGeneratorActive;
        }
    }

    IEnumerator ConsumeFuel()
    {
        if (ship.fuel > 0)
        {
            ship.fuel -= fuelConsumptionRate;
            yield return new WaitForSeconds(fuelConsumptionTickRate);
            StartCoroutine(ConsumeFuel());
        }
        else
        {
            yield return new WaitForSeconds(fuelConsumptionTickRate);
            StartCoroutine(ConsumeFuel());
        }
    }
}
