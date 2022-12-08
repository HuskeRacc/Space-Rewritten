using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipErrors : MonoBehaviour
{
    [SerializeField] PowerGenerator power;
    [SerializeField] OxygenGenerator oxygen;

    [SerializeField] bool canError = false;

    [SerializeField] float RNGCooldown = 3;
    [SerializeField] float RNGPowerTarget = 5;
    [SerializeField] float RNGOxygenTarget = 2;
    [SerializeField] float RNGResult;
    [SerializeField] float RNGStartupWait = 10f;

    [SerializeField] float minRNGResult = 1;
    [SerializeField] float maxRNGResult = 12;

    private void Start()
    {
        StartCoroutine(StopRNG());
        StartCoroutine(RNGSystem());
    }

    private void Update()
    {
        if (canError)
        {
            PowerErrorCheck();
            OxygenErrorCheck();
        }
    }

    void PowerErrorCheck()
    {
        if (RNGResult == RNGPowerTarget)
        {
            PowerError();
        }
    }

    void OxygenErrorCheck()
    {
        if (RNGResult == RNGOxygenTarget && canError)
        {
            OxygenError();
        }
    }

    void PowerError()
    {
        power.powerGeneratorActive = false;
        power.powerGeneratorAvailable = true;
        StartCoroutine(ErrorCooldown(240));
        Debug.Log("Power Error Triggered");
    }

    void OxygenError()
    {
        oxygen.o2GeneratorActive = false;
        StartCoroutine(ErrorCooldown(120));
        Debug.Log("Oxygen Error Triggered");
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
