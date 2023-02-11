using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipSystems : MonoBehaviour
{
    public static ShipSystems instance;

    [Header("Oxygen")]
    public float shipOxygen = 100;
    public float maxOxygen = 100f;
    public bool noOxygen = false;

    [Header("Fuel")]
    [Range(0,100)] public float fuel = 100f;

    [Header("Solars")]
    public bool solarsActive = true;
    public float solarEfficiency = 100f;
    public float solarDecreaseRate = .1f;
    public float solarDecreaseTimer = 12f;

    [Header("Battery")]
    public float shipBattery = 100f;
    public float batteryDecreaseRate = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(DecreaseSolarEfficiency), 0, solarDecreaseTimer);
        InvokeRepeating(nameof(ChargeBattery), 0, 1);
    }

    private void Update()
    {
        HandleClamps();
    }

    void HandleClamps()
    {
        if(shipOxygen > maxOxygen)
        {
            shipOxygen = maxOxygen;
        }

        if (shipOxygen <= 0)
        {
            shipOxygen = 0;
            noOxygen = true;
        }
        else
        {
            noOxygen = false;
        }

        if (shipBattery > 100)
        {
            shipBattery = 100f;
        }

        if(shipBattery < 0)
        {
            shipBattery = 0;
        }
    }

    void SolarActiveCheck()
    {
        if(solarEfficiency == 0)
        {
            solarsActive = false;
        }
    }

    void ChargeBattery()
    {
        if (solarEfficiency >= 50)
        {
            if (solarsActive)
                shipBattery += Random.Range(0.5f, 1.5f);
            else shipBattery += 0.00f;
        }
        if (solarEfficiency < 50)
        {
            if (solarsActive)
                shipBattery += Random.Range(0.015f, 0.45f);
            else shipBattery += 0;
        }
    }

    void DecreaseSolarEfficiency()
    {
        solarEfficiency -= solarDecreaseRate;
    }

    void DecreaseBattery()
    {
        if(solarEfficiency < 50 && solarsActive)
        {
            shipBattery -= 0.01f;
        }
    }

    public void Enable()
    {
        solarsActive = true;
    }

    public void Disable()
    {
        solarsActive = false;
    }
}
