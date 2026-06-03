using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDamageManager : MonoBehaviour
{

    public static DroneDamageManager instance;

    public bool jawsDamaged = false;
    public bool cargoDamaged = false;
    public bool chassisDamaged = false;
    public bool thrustersDamaged = false;

    [SerializeField] int maxCountdown = 120;

    [SerializeField] int[] rngTargets;
    
    bool checkingRNG;

    [Header("Debug")]
    [SerializeField] int droneDamageRNGResult = 0;
    [SerializeField] int countdownToCheck;
    int indexRNG;

    [Header("Debug Travel Time")]
    float savedTravelTime;
    bool travelTimeSaved = false;
    int travelTimeReducedAmount = 0;

    [Header("Debug Material Rate")]
     float savedMaterialGainRate;
     float savedMaterialGainRateMode1;
     float savedMaterialGainRateMode2;
     bool materialGainRateSaved = false;
     int materialGainRateReducedAmount = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(Countdown), 1, 1);
    }

    void DamageJaws()
    {
        ReduceMaterialGain();
        jawsDamaged = true;
    }

    void DamageCargo()
    {
        ReduceMaterialGain();
        cargoDamaged = true;
    }

    void DamageChassis()
    {
        IncreaseTravelTime();
        chassisDamaged = true;
    }

    void DamageThrusters()
    {
        IncreaseTravelTime();
        thrustersDamaged = true;
    }

    void ReduceMaterialGain()
    {
        if(materialGainRateReducedAmount < 2)
        {
            if(!materialGainRateSaved)
            {
                savedMaterialGainRate = DroneManager.instance.maximumMineable;
                savedMaterialGainRateMode1 = DroneManager.instance.satModeMaxMineable;
                savedMaterialGainRateMode2 = DroneManager.instance.fuelModeMaxMineable;
                Debug.Log("Saved Material Gain rates");
                materialGainRateSaved = true;
            }

            DroneManager.instance.maximumMineable /= 2;
            DroneManager.instance.satModeMaxMineable /= 2;
            DroneManager.instance.fuelModeMaxMineable /= 2;
            Debug.Log("Reduced all material gain rates divided by 2");
            materialGainRateReducedAmount++;
        }
        else
        {
            Debug.Log("Tried to reduce material gain rate but it's already been reduced twice!");
        }
    }

    public void ResetMaterialGain()
    {
        if(!jawsDamaged && !cargoDamaged)
        {
            DroneManager.instance.maximumMineable = savedMaterialGainRate;
            DroneManager.instance.satModeMaxMineable = savedMaterialGainRateMode1;
            DroneManager.instance.fuelModeMaxMineable = savedMaterialGainRateMode2;

            savedMaterialGainRate = 0;
            savedMaterialGainRateMode1 = 0;
            savedMaterialGainRateMode2 = 0;
            materialGainRateSaved = false;
            materialGainRateReducedAmount = 0;
            Debug.Log(
                "Material Gain Rate Reset | Mode 0: " 
                + DroneManager.instance.maximumMineable 
                + " Mode 1: " + DroneManager.instance.satModeMaxMineable + " Mode 2: " 
                + DroneManager.instance.fuelModeMaxMineable
                );
        }
    }

    void IncreaseTravelTime()
    {
        if(travelTimeReducedAmount < 2)
        {
            if (!travelTimeSaved)
            {
                savedTravelTime = DroneManager.instance.timeToTravel;
                Debug.Log("Saved travel time as: " + savedTravelTime);
                travelTimeSaved = true;
            }

            DroneManager.instance.timeToTravel *= 2;
            Debug.Log("Increased Travel Time by x2: " + DroneManager.instance.timeToTravel);
            travelTimeReducedAmount++;
        }
        else
        {
            Debug.Log("Tried to reduce travel time but it's already been reduced twice!");
        }
    }

    public void ResetTravelTime()
    {
        if (!thrustersDamaged && !chassisDamaged)
        {
            DroneManager.instance.timeToTravel = savedTravelTime;
            savedTravelTime = 0;
            travelTimeSaved = false;
            travelTimeReducedAmount = 0;
            Debug.Log("Travel time reset: " + DroneManager.instance.timeToTravel);
        }
    }

    private void Update()
    {
        if(countdownToCheck >= maxCountdown)
        {
            Debug.Log("Countdown finished. Rolling Damage RNG");
            if (checkingRNG == false) // failsafe so the RNG doesnt run more than once.
            {
                if (DroneManager.instance != null && DroneManager.instance.IsMining)
                {
                    DamageRNG();
                }
                else
                {
                    droneDamageRNGResult = 0;
                    checkingRNG = false;
                }
            }
            countdownToCheck = 0;
        }
    }

    void Countdown()
    {
        if(DroneManager.instance != null && DroneManager.instance.IsMining)
            countdownToCheck++;
    }

    void DamageRNG()
    {
        checkingRNG = true;
        indexRNG = Random.Range(0, rngTargets.Length);
        droneDamageRNGResult = rngTargets[indexRNG];
        Debug.Log("RNG RESULT: " + rngTargets[indexRNG]);
        DamageChance();
    }

    public void DamageChance()
    {
        if (droneDamageRNGResult == rngTargets[0])
        {
            //Jaws
            DamageJaws();
            droneDamageRNGResult = 0;
        }
        if (droneDamageRNGResult == rngTargets[1])
        {
            //Cargo
            DamageCargo();
            droneDamageRNGResult = 0;
        }
        if (droneDamageRNGResult == rngTargets[2])
        {
            //Chassis
            DamageChassis();
            droneDamageRNGResult = 0;
        }
        if (droneDamageRNGResult == rngTargets[3])
        {
            //Thruster
            DamageThrusters();
            droneDamageRNGResult = 0;
        }
        checkingRNG = false;
    }

}
