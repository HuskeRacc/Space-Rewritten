using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneManager : MonoBehaviour
{

    public static DroneManager instance;

    [HideInInspector] public string droneStatus;
    [HideInInspector] public string buttonStatus;

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
    public int minimumBatteryChargeTime = 3;
    public int maximumBatteryChargeTime = 6;

    [Header("Mined Value")]
    public float minimumMineable = 0.1f;
    public float maximumMineable = 1.0f;
    public float mode1MinMineable = 1.0f;
    public float mode1MaxMineable = 1.0f;
    public float mode2MinMineable = 1.0f;
    public float mode2MaxMineable = 1.0f;

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
        Idle();
    }

    public void Idle()
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].interactable = true;
        }

        anim.SetBool("TakeOff", false);
        anim.SetBool("Recall", true);

        BankMaterials();

        status = 0;

        droneStatus = "Idle";
        buttonStatus = "Send?";

        InvokeRepeating(nameof(ChargeBattery), 0, Random.Range(minimumBatteryChargeTime, maximumBatteryChargeTime));
    }

    public void Sending()
    {
        for (int i = 0; i < modeButtons.Length; i++)
        {
            modeButtons[i].interactable = false;
        }

        anim.SetBool("TakeOff",true);
        status = 1;
        droneStatus = "En-Route";
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
            InvokeRepeating(nameof(Mode0MaterialGain), 4, Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        if (mode == 1)
        {
            InvokeRepeating(nameof(Mode1MaterialGain), 4, Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        if(mode== 2)
        {
            InvokeRepeating(nameof(Mode2MaterialGain), 4, Random.Range(MinimumMaterialGainTime, MaximumMaterialGainTime));
        }
        InvokeRepeating(nameof(DepleteBattery), 0, Random.Range(minimumBatteryDepletionTime, maximumBatteryDepletionTime));
    }

    public void Returning()
    {
        if(battery == 0)
        {
            droneStatus = "Out of Fuel, Returning.";
            anim.SetBool("TakeOff", false);
            anim.SetBool("Recall", true);
            status = 3;
            buttonStatus = "Please Wait...";
            CancelInvoke();
            Invoke(nameof(Idle), timeToTravel);
        }
        else
        {
            droneStatus = "Returning";
            anim.SetBool("TakeOff", false);
            anim.SetBool("Recall", true);
            status = 3;
            buttonStatus = "Please Wait...";
            CancelInvoke();
            Invoke(nameof(Idle), timeToTravel);
        }
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

    void Mode0MaterialGain()
    {
        float satonium = Random.Range(minimumMineable,maximumMineable);
        float thrustium = Random.Range(thrustiumMinimumMineable, thrustiumMaximumMineable);
        float fuelium = Random.Range(minimumMineable,maximumMineable);

        satoniumAmount += satonium;
        fueliumAmount += fuelium;
        thrustiumAmount += thrustium;
    }

    void Mode1MaterialGain()
    {
        float satonium = Random.Range(mode1MinMineable, mode1MaxMineable);
        
        satoniumAmount += satonium;
    }

    void Mode2MaterialGain()
    {
        float fuelium = Random.Range(mode2MinMineable, mode2MaxMineable);

        fueliumAmount += fuelium;
    }

    void ChargeBattery()
    {
        battery += batteryDepletionRate;
    }

    void DepleteBattery()
    {
        if (battery == 0)
        {
            Returning();
        }
        else
        {
            battery -= batteryDepletionRate;
        }
    }
}
