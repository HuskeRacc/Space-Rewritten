using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] PlayerStatus playerStatus;

    [HideInInspector] public string droneStatus;
    [HideInInspector] public string buttonStatus;

    public float timeToTravel = 30f;

    [Header("Drone Status")]
    public int status = 0;

    public float droneHeldCurrency;

    public float currencyToTransfer;

    public float battery = 100f;

    [Header("Currency Gain")]
    public int minimumCurrencyGainTime = 3;
    public int maximumCurrencyGainTime = 15;

    [Header("Battery Depletion")]
    [SerializeField] float batteryDepletionRate = 2f;
    public int minimumBatteryDepletionTime = 1;
    public int maximumBatteryDepletionTime = 3;

    [Header("Battery Charging")]
    public int minimumBatteryChargeTime = 3;
    public int maximumBatteryChargeTime = 6;

    [Header("Mined Value")]
    public float minimumMineable = 0.1f;
    public float maximumMineable = 1.0f;

    private void Start()
    {
        Idle();
    }

    public void Idle()
    {
        TransferCurrency();

        status = 0;
        droneStatus = "Idle";
        buttonStatus = "Send?";

        InvokeRepeating(nameof(ChargeBattery), 0, Random.Range(minimumBatteryChargeTime, maximumBatteryChargeTime));
    }

    public void Sending()
    {
        status = 1;
        droneStatus = "En-Route";
        buttonStatus = "Please Wait...";
        CancelInvoke(nameof(ChargeBattery));
        Invoke(nameof(OnPlanet), timeToTravel);
    }

    public void OnPlanet()
    {
        status = 2;
        droneStatus = "Mining";
        buttonStatus = "Return?";
        InvokeRepeating(nameof(DroneCurrencyGain), 0, Random.Range(minimumCurrencyGainTime,maximumCurrencyGainTime));
        InvokeRepeating(nameof(DepleteBattery), 0, Random.Range(minimumBatteryDepletionTime, maximumBatteryDepletionTime));
    }

    public void Returning()
    {
        status = 3;
        droneStatus = "Returning";
        buttonStatus = "Please Wait...";
        currencyToTransfer = droneHeldCurrency;
        CancelInvoke();
        Invoke(nameof(Idle), timeToTravel);
    }

    void TransferCurrency()
    {
        if (currencyToTransfer != 0)
        {
            droneHeldCurrency = 0;
            playerCurrency.currency += currencyToTransfer;
            StartCoroutine(playerStatus.TextPopup("$" + currencyToTransfer.ToString("F2") + " was transferred to your account.", 5));
            currencyToTransfer = 0;
        }
    }

    void DroneCurrencyGain()
    {
        float amountToMine = Random.Range(minimumMineable,maximumMineable);
        droneHeldCurrency += amountToMine;
    }

    void ChargeBattery()
    {
        battery += batteryDepletionRate;
    }

    void DepleteBattery()
    {
        battery -= batteryDepletionRate;
    }
}
