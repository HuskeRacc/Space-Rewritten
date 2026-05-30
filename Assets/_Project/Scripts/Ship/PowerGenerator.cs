using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerGenerator : Interactable, ISaveable
{

    public bool powerGeneratorActive;
    public bool powerGeneratorAvailable;

    [SerializeField] AudioSource powerSound;
    [SerializeField] AudioSource refuelAudioSource;
    [SerializeField] AudioSource toggleSoundSource;
    [SerializeField] AudioClip refuelAudioClip;
    [SerializeField] AudioClip toggleAudioClip;
    [SerializeField] OxygenGenerator oxygen;
    [SerializeField] ShipSystems ship;
    [SerializeField] PlayerStatus status;

    [SerializeField] Animator anim;

    public float fuelConsumptionRate = 2f;
    [SerializeField] float fuelConsumptionTickRate = 5f;

    [SerializeField] private List<LightManager> lightManagers = new();
    private bool previousPowerState;

    [SerializeField] GameObject lightIndicator;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();

        previousPowerState = powerGeneratorActive;
        SetAllLightsPower(powerGeneratorActive);

        InvokeRepeating(nameof(ConsumeFuel), 0, fuelConsumptionTickRate);
    }

    private void Update()
    {
        FuelClamps();
        HandlePower();
        HandleAnimation();
    }

    void HandleAnimation()
    {
        if (powerGeneratorActive)
            anim.SetBool("togglestate", true);
        else
            anim.SetBool("togglestate", false);
    }

    void FuelClamps()
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


    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        if (ship.fuel > 0)
        {
            TogglePowerGenerator();
        }
        else
        {
            StartCoroutine(status.TextPopup("No Fuel.", 5, false));
        }
    }

    public override void OnLoseFocus()
    {
    }

    public void HandlePower()
    {
        if (powerGeneratorActive != previousPowerState)
        {
            SetAllLightsPower(powerGeneratorActive);
            previousPowerState = powerGeneratorActive;
        }

        if (powerGeneratorActive)
        {
            lightIndicator.GetComponent<Renderer>().material.color = Color.green;
            powerSound.UnPause();
        }
        else
        {
            lightIndicator.GetComponent<Renderer>().material.color = Color.red;
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

    private void SetAllLightsPower(bool hasPower)
    {
        Debug.Log("Setting all lights power to: " + hasPower + ". Light count: " + lightManagers.Count);

        for (int i = 0; i < lightManagers.Count; i++)
        {
            if (lightManagers[i] != null)
            {
                lightManagers[i].SetPower(hasPower);
            }
            else
            {
                Debug.LogWarning("LightManager at index " + i + " is null.");
            }
        }
    }

    public void TogglePowerGenerator()
    {
        if (powerGeneratorAvailable)
        {
            toggleSoundSource.PlayOneShot(toggleAudioClip);
            powerGeneratorActive = !powerGeneratorActive;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("fuelCan"))
        {
            Debug.Log("Generator Refueled!");
            refuelAudioSource.PlayOneShot(refuelAudioClip);
            Destroy(other.gameObject);
            ship.fuel += 75f;
        }
    }

    void ConsumeFuel()
    {
        if (ship.fuel > 0 && powerGeneratorActive)
        {
            ship.fuel -= fuelConsumptionRate;
        }
    }

    public object CaptureState()
    {
        return new SaveData
        {
            savedPowerStatus = powerGeneratorActive
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        powerGeneratorActive = saveData.savedPowerStatus;
    }

    [Serializable]
    private struct SaveData
    {
        public bool savedPowerStatus;
    }
}
