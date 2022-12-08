using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipSystems : MonoBehaviour
{
    [Header("Oxygen")]
    public float shipOxygen = 100;
    public float maxOxygen = 100f;
    public bool noOxygen = false;

    [Header("Fuel")]
    [Range(0,100)] public float fuel = 100f;

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
            noOxygen = true;
        }
        else
        {
            noOxygen = false;
        }
    }
}
