using TMPro;
using UnityEngine;

public class PowerGenerator : Interactable
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

    public float fuelConsumptionRate = 2f;
    [SerializeField] float fuelConsumptionTickRate = 5f;

    [SerializeField] GameObject lightIndicator;

    private void Start()
    {
        InvokeRepeating(nameof(ConsumeFuel), 0, fuelConsumptionTickRate);
    }

    private void Update()
    {
        FuelClamps();
        HandlePower();
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
            lightIndicator.GetComponent<Renderer>().material.color = Color.green;
            powerSound.UnPause();
        }
        if (!powerGeneratorActive)
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

    void ConsumeFuel()
    {
        if (ship.fuel > 0 && powerGeneratorActive)
        {
            ship.fuel -= fuelConsumptionRate;
        }
    }
}
