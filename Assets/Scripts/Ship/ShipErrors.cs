using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipErrors : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] OxygenGenerator oxygen;
    [SerializeField] PowerGenerator power;

    [Header("General")]
    [SerializeField] bool canError = false;

    [Header("RNG Targets")]
    [SerializeField] float RNGCooldown = 3;

    [SerializeField] int[] targetsForRNG;

    [Header("RNG Variables")]
    [SerializeField] int RNGStartupWait = 10;
    [SerializeField] int RNGResult;
    [SerializeField] int minRNGResult = 1;
    [SerializeField] int maxRNGResult = 12;
    [SerializeField] int failedRNGChecks = 0;

    private void Start()
    {
        StartCoroutine(StopRNG());
        StartCoroutine(RNGSystem());
    }

    private void Update()
    {
        if (canError)
        {
            ErrorCheck();
        }
    }

    void ErrorCheck()
    {
        if (RNGResult == targetsForRNG[0])
        {
            PowerError();
        }
        else return;

        if (RNGResult == targetsForRNG[1])
        {
            OxygenError();
        }
        else return;

        if(RNGResult == targetsForRNG[2])
        {
            SolarError();   
        }

        RNGResult = 0;
    }

    void PowerError()
    {
        ErrorNotificationSystem.instance.GeneratorError();
        power.powerGeneratorActive = false;
        power.powerGeneratorAvailable = true;
        StartCoroutine(ErrorCooldown(240));
        Debug.Log("Power Error Triggered");
    }

    void OxygenError()
    {
        ErrorNotificationSystem.instance.OxygenError();
        oxygen.o2GeneratorActive = false;
        StartCoroutine(ErrorCooldown(120));
        Debug.Log("Oxygen Error Triggered");
    }

    void SolarError()
    {
        ErrorNotificationSystem.instance.SolarError();
        ShipSystems.instance.solarsActive = false;
        StartCoroutine(ErrorCooldown(120));
        Debug.Log("Solars Error Triggered");
    }


    IEnumerator RNGSystem()
    {
        if (canError)
        {
            RNGResult = Random.Range(minRNGResult, maxRNGResult);
        }
            yield return new WaitForSeconds(RNGCooldown);
            StartCoroutine(RNGSystem());

    }

    IEnumerator StopRNG()
    {
        canError = false;
        yield return new WaitForSeconds(RNGStartupWait);
        canError = true;
    }

    IEnumerator ErrorCooldown(float errorCooldown)
    {
        canError = false;
        yield return new WaitForSeconds(errorCooldown);
        canError = true;
    }
}
