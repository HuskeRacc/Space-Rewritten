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
    [SerializeField] GameObject[] lightSwitches;
    [SerializeField] List<LightManager> lightManager;
    [SerializeField] bool lightsOff = false;

    [SerializeField] Animator anim;

    public float fuelConsumptionRate = 2f;
    [SerializeField] float fuelConsumptionTickRate = 5f;

    [SerializeField] GameObject lightIndicator;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        lightSwitches = GameObject.FindGameObjectsWithTag("lightswitch");

        for (int i = 0; i < lightSwitches.Length; i++)
        {
            lightManager.Add(lightSwitches[i].GetComponent<LightManager>());
        }

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

    public void HandlePower()
    {
        if (powerGeneratorActive)
        {
            lightsOff = false;
            lightIndicator.GetComponent<Renderer>().material.color = Color.green;
            powerSound.UnPause();
        }
        if (!powerGeneratorActive)
        {
            if (!lightsOff)
                TurnOffAllLights();
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
            StartCoroutine(status.TextPopup("No Fuel.", 5, false));
        }
    }

    public override void OnLoseFocus()
    {
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

    private void TurnOffAllLights()
    {
        for (int i = 0; i < lightManager.Count; i++)
        {
            lightManager[i].ToggleLightOff();
        }
        lightsOff = true;
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
