using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelmMenu : MonoBehaviour
{

    public static HelmMenu instance;

    [SerializeField] PlayerMovement player;
    [SerializeField] DroneManager droneManager;

    [SerializeField] TextMeshProUGUI statusValue;
    [SerializeField] TextMeshProUGUI sendDroneButtonTXT;
    [SerializeField] TextMeshProUGUI batteryValue;

    [SerializeField] TextMeshProUGUI travelTimeLeftValue;
    [SerializeField] float timeToTravelCalculated;

    [SerializeField] TextMeshProUGUI satoniumValue;
    [SerializeField] TextMeshProUGUI thrustiumValue;
    [SerializeField] TextMeshProUGUI fueliumValue;

    [SerializeField] TextMeshProUGUI TravelStatusBankedTHR;

    [SerializeField] Button travelButton;

    [SerializeField] GameObject helmMenu;

    [SerializeField] TextMeshProUGUI fuelModeTXT;
    [SerializeField] TextMeshProUGUI satModeTXT;
    [SerializeField] TextMeshProUGUI anyModeTXT;

    [SerializeField] TextMeshProUGUI travelStatusTXT;

    [SerializeField] float thrustiumRequiredForNextStation = 250f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ValueUpdateText());
    }

    public void OnClick_Back()
    {
        helmMenu.SetActive(false);
        player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnClick_SendDrone()
    {
        if (droneManager.status == 0)
        {
            droneManager.Sending();
        }

        if (droneManager.status == 2)
        {
            droneManager.Returning();
        }

        if (droneManager.status == 4)
        {
            droneManager.Returning();
        }
    }

    public void OnClick_AnyMode()
    {
        DroneManager.instance.mode = 0;
        ResetButtonText();
        anyModeTXT.text = "SET";
    }

    public void OnClick_FuelMode()
    {
        DroneManager.instance.mode = 1;
        ResetButtonText();
        fuelModeTXT.text = "SET";

    }
    public void OnClick_SatMode()
    {
        DroneManager.instance.mode = 2;
        ResetButtonText();
        satModeTXT.text = "SET";
    }

    private void ResetButtonText()
    {
        satModeTXT.text = "SET MODE";
        fuelModeTXT.text = "SET MODE";
        anyModeTXT.text = "SET MODE";
    }

    public void Onclick_Travel()
    {
        if(ShipMaterialBank.instance.thrustiumBanked >= 250)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Update()
    {
        DisplayTextValues();
        DisplayDroneValues();

        if (ShipMaterialBank.instance.thrustiumBanked >= 250)
        {
            travelButton.interactable = true;
        }
        else
        {
            travelButton.interactable = false;
        }
    }

   IEnumerator ValueUpdateText()
    {
        Debug.Log("Travel status update check");
        UpdateTravelStatus();
        TravelStatusBankedTHR.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F0") + " / " + thrustiumRequiredForNextStation.ToString("F0");
        yield return new WaitForSeconds(5);
        StartCoroutine(ValueUpdateText());
    }

    void UpdateTravelStatus()
    {
        if(ShipMaterialBank.instance.thrustiumBanked >= 250)
        {
            travelStatusTXT.color = Color.green;
            travelStatusTXT.text = "Travel Status: READY";
        }
        else
        {
            travelStatusTXT.color = Color.red;
            travelStatusTXT.text = "Travel Status: NOT READY";
        }
    }

    void DisplayTextValues()
    {
        statusValue.text = droneManager.droneStatus;
        sendDroneButtonTXT.text = droneManager.buttonStatus;
        batteryValue.text = droneManager.battery.ToString("F0") + "%";
    }

    void DisplayDroneValues()
    {
        satoniumValue.text = DroneManager.instance.satoniumAmount.ToString("F2");
        thrustiumValue.text = DroneManager.instance.thrustiumAmount.ToString("F2");
        fueliumValue.text = DroneManager.instance.fueliumAmount.ToString("F2");
    }

    public void DisplayTravelTime(float timeToTravel)
    {
        timeToTravelCalculated = timeToTravel;
        travelTimeLeftValue.text = timeToTravelCalculated.ToString("F0");
        InvokeRepeating(nameof(CalculateTravelTimeLeft), 0, 1);
        travelTimeLeftValue.gameObject.SetActive(true);
    }

    void CalculateTravelTimeLeft()
    {
        if (timeToTravelCalculated <= 0)
        {
            travelTimeLeftValue.gameObject.SetActive(false);
            CancelInvoke(nameof(CalculateTravelTimeLeft));
        }
        travelTimeLeftValue.text = timeToTravelCalculated.ToString("F0");
        timeToTravelCalculated--;
    }
}
