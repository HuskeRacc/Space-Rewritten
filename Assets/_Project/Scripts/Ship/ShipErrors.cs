using System.Collections;
using UnityEngine;

public class ShipErrors : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] OxygenGenerator oxygen;
    [SerializeField] PowerGenerator power;
    [SerializeField] Radio radio;
    GameObject[] lightsGO;
    LightManager lights;

    [Header("General")]
    [SerializeField] bool canError = false;

    [Header("RNG Targets")]
    [SerializeField] int[] targetsForRNG;

    [Header("RNG Variables")]
    [SerializeField] float timeBetweenChecks = 3;
    [SerializeField] float errorCooldown = 240;
    [SerializeField] int RNGStartupWait = 10;
    [SerializeField] int RNGResult;
    [SerializeField] int minRNGResult = 1;
    [SerializeField] int maxRNGResult = 12;
    [SerializeField] int failedRNGChecks = 0;
    [SerializeField] int maxFailedAmount = 10;

    [Header("Debug")]
    [SerializeField] bool powerError;
    [SerializeField] bool oxygenError;
    [SerializeField] bool solarError;
    [SerializeField] bool lightError;
    [SerializeField] bool radioError;

    private void Start()
    {
        lightsGO = GameObject.FindGameObjectsWithTag("lightswitch");
        StartCoroutine(RNGStartup());
        StartCoroutine(RNGSystem());
        InvokeRepeating(nameof(ErrorCheck), RNGStartupWait, timeBetweenChecks);
    }

    private void Update()
    {
        if (powerError)
        {
            PowerError();
            powerError = false;
        }

        if (oxygenError)
        {
            OxygenError();
            oxygenError = false;
        }

        if (solarError)
        {
            SolarError();
            solarError = false;
        }

        if (lightError)
        {
            LightError();
            lightError = false;
        }

        if (radioError)
        {
            RadioError();
            radioError = false;
        }
    }

    void ErrorCheck()
    {

        if (RNGResult == targetsForRNG[0])
        {
            PowerError();
        }

        if (RNGResult == targetsForRNG[1])
        {
            OxygenError();
        }

        if (RNGResult == targetsForRNG[2])
        {
            SolarError();
        }

        if (RNGResult == targetsForRNG[3])
        {
            LightError();
        }

        if (RNGResult == targetsForRNG[4])
        {
            RadioError();
        }

        if (failedRNGChecks >= maxFailedAmount)
        {
            RNGResult = targetsForRNG[Random.Range(0,3)];
        }
        else if(canError)
        {
            RNGResult = 0;
            failedRNGChecks++;
        }
    }

    void RadioError()
    {
        radio.ToggleMusicSpooky();
        errorCooldown = 240;
        StartCoroutine(ErrorCooldown(errorCooldown));
        Debug.Log("Radio Error Triggered");
        failedRNGChecks = 0;
        RNGResult = 0;
    }

    void PowerError()
    {
        ErrorNotificationSystem.instance.GeneratorError();
        power.powerGeneratorActive = false;
        power.powerGeneratorAvailable = true;
        errorCooldown = 240;
        StartCoroutine(ErrorCooldown(errorCooldown));
        Debug.Log("Power Error Triggered");
        failedRNGChecks = 0;
        RNGResult = 0;
    }

    void OxygenError()
    {
        ErrorNotificationSystem.instance.OxygenError();
        oxygen.o2GeneratorActive = false;
        errorCooldown = 120;
        StartCoroutine(ErrorCooldown(errorCooldown));
        Debug.Log("Oxygen Error Triggered");
        failedRNGChecks = 0;
        RNGResult = 0;
    }

    void SolarError()
    {
        ErrorNotificationSystem.instance.SolarError();
        ShipSystems.instance.solarsActive = false;
        errorCooldown = 120;
        StartCoroutine(ErrorCooldown(errorCooldown));
        Debug.Log("Solars Error Triggered");
        failedRNGChecks = 0;
        RNGResult = 0;
    }

    void LightError()
    {
        ErrorNotificationSystem.instance.LightError();

        for (int i = 0; i < lightsGO.Length; i++)
        {
            LightManager light = lightsGO[i].GetComponent<LightManager>();

            if (light != null)
            {
                light.SetLightError(true);
            }
        }

        errorCooldown = 120;
        StartCoroutine(LightErrorDuration(errorCooldown));
        StartCoroutine(ErrorCooldown(errorCooldown));

        Debug.Log("Light Error Triggered");
        failedRNGChecks = 0;
        RNGResult = 0;
    }

    void ClearLightError()
    {
        for (int i = 0; i < lightsGO.Length; i++)
        {
            LightManager light = lightsGO[i].GetComponent<LightManager>();

            if(light != null)
            {
                light.SetLightError(false);
            }
        }
        Debug.Log("Light error cleared");
    }


    IEnumerator RNGSystem()
    {
        if (canError)
        {
            RNGResult = Random.Range(minRNGResult, maxRNGResult);
        }
            yield return new WaitForSeconds(timeBetweenChecks);
            StartCoroutine(RNGSystem());
    }

    IEnumerator RNGStartup()
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

    IEnumerator LightErrorDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        ClearLightError();
    }
}
