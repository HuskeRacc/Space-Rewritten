using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneManager : MonoBehaviour, ISaveable
{

    public static DroneManager instance;

    [HideInInspector] public string droneStatus;
    [HideInInspector] public string buttonStatus;

    [SerializeField] AudioSource droneAudioSource;
    [SerializeField] bool init = true;
    [SerializeField] AudioClip[] droneSounds;

    [SerializeField] Animator anim;

    public float timeToTravel = 30f;

    [SerializeField] Button[] modeButtons;

    [Header("Drone Status")]
    public int status = 0;

    public int mode = 0;

    public float battery = 100f;

    [Header("Materials")]
    public float satoniumAmount;
    public float thrustiumAmount;
    public float fueliumAmount;

    public int MinimumMaterialGainTime = 3;
    public int MaximumMaterialGainTime = 15;

    public int maxBatteryCharge = 100;

    [Header("Battery Depletion")]
    public float batteryDepletionRate = 4f;
    public int minimumBatteryDepletionTime = 1;
    public int maximumBatteryDepletionTime = 3;

    [Header("Battery Charging")]
    public float batteryChargeRate = 4f;
    public int minimumBatteryChargeTime = 3;
    public int maximumBatteryChargeTime = 6;

    [Header("Mined Value")]
    public float minimumMineable = 0.1f;
    public float maximumMineable = 1.0f;
    public float satModeMinMineable = 1.0f;
    public float satModeMaxMineable = 1.0f;
    public float fuelModeMinMineable = 1.0f;
    public float fuelModeMaxMineable = 1.0f;

    public float thrustiumMinimumMineable = 0.01f;
    public float thrustiumMaximumMineable = 0.02f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (battery >= maxBatteryCharge)
            battery = maxBatteryCharge;
        if (battery <= 0)
            battery = 0;
    }

    private void Start()
    {
        StartCoroutine(droneSoundEnable());
        Idle();
    }

    IEnumerator droneSoundEnable()
    {
        init = true;
        yield return new WaitForSeconds(5);
        init = false;
    }

    public void Idle()
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].interactable = true;
        }

        if (init == false)
            droneAudioSource.PlayOneShot(droneSounds[1]);

        anim.SetBool("TakeOff", false);
        anim.SetBool("Recall", true);

        BankMaterials();

        status = 0;

        droneStatus = "Idle";
        buttonStatus = "Send";

        InvokeRepeating(nameof(ChargeBattery), 0, UnityEngine.Random.Range(minimumBatteryChargeTime, maximumBatteryChargeTime));
    }

    public void Sending()
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].interactable = false;
        }

        anim.SetBool("TakeOff",true);
        status = 1;
        DisplayTravelTime();
        droneStatus = "En-Route";
        droneAudioSource.PlayOneShot(droneSounds[0]);
        buttonStatus = "Please Wait...";
        CancelInvoke(nameof(ChargeBattery));
        Invoke(nameof(Mining), timeToTravel);
    }

    public void Mining()
    {
        status = 2;
        droneStatus = "Mining";
        buttonStatus = "Return?";

        if (mode == 0)
        {
            InvokeRepeating(nameof(AnyMaterialGain), 4, UnityEngine.Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        if (mode == 1)
        {
            InvokeRepeating(nameof(SatMaterialGain), 4, UnityEngine.Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        if(mode== 2)
        {
            InvokeRepeating(nameof(FuelMaterialGain), 4, UnityEngine.Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        InvokeRepeating(nameof(DepleteBattery), 0, UnityEngine.Random.Range(minimumBatteryDepletionTime, maximumBatteryDepletionTime));
    }

    public void Returning()
    {
        droneStatus = "Returning";
        anim.SetBool("TakeOff", false);
        anim.SetBool("Recall", true);
        status = 3;
        DisplayTravelTime();
        buttonStatus = "Please Wait...";
        CancelInvoke();
        Invoke(nameof(Idle), timeToTravel);
    }

    public void WaitingForReturn()
    {
        Debug.Log("Drone out of battery");
        droneStatus = "Out of Power.";
        status = 4;
        buttonStatus = "Return?";
        CancelInvoke();
    }

    void BankMaterials()
    {
        ShipMaterialBank.instance.satoniumBanked += satoniumAmount;
        ShipMaterialBank.instance.thrustiumBanked += thrustiumAmount;
        ShipMaterialBank.instance.fueliumBanked += fueliumAmount;
        satoniumAmount = 0;
        thrustiumAmount = 0;
        fueliumAmount = 0;
    }

    void AnyMaterialGain()
    {
        float satonium = UnityEngine.Random.Range(minimumMineable,maximumMineable);
        float thrustium = UnityEngine.Random.Range(thrustiumMinimumMineable, thrustiumMaximumMineable);
        float fuelium = UnityEngine.Random.Range(minimumMineable,maximumMineable);

        satoniumAmount += satonium;
        fueliumAmount += fuelium;
        thrustiumAmount += thrustium;
    }

    void SatMaterialGain()
    {
        float satonium = UnityEngine.Random.Range(satModeMinMineable, satModeMaxMineable);
        
        satoniumAmount += satonium;
    }

    void FuelMaterialGain()
    {
        float fuelium = UnityEngine.Random.Range(fuelModeMinMineable, fuelModeMaxMineable);

        fueliumAmount += fuelium;
    }

    void ChargeBattery()
    {
        if(ShipSystems.instance.shipBattery > batteryChargeRate)
        {
            if(battery < maxBatteryCharge)
            {
                battery += batteryChargeRate;

                ShipSystems.instance.shipBattery -= batteryChargeRate / 3;
            }
        }
        else
        {
            return;
        }
    }

    void DepleteBattery()
    {
        if (battery <= 0)
        {
            WaitingForReturn();
        }
        else
        {
            battery -= batteryDepletionRate;
        }
    }

    public void DisplayTravelTime()
    {
        HelmMenu.instance.DisplayTravelTime(timeToTravel);
    }

    public object CaptureState()
    {
        return new SaveData
        {
            
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

    }

    [Serializable]
    private struct SaveData
    {
        
    }
}
