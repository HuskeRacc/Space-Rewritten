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

    [SerializeField] float requiredThrustium = 250f;

    [Header("Mode Button Graphics")]
    [SerializeField] Image fuelModePanel;
    [SerializeField] Image satModePanel;
    [SerializeField] Image anyModePanel;

    [SerializeField] Color modeAvailableColor = Color.green;
    [SerializeField] Color modeUnavailableColor = Color.red;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(ValueUpdateText());
    }
    private void Update()
    {
        DisplayTextValues();
        DisplayDroneValues();
        UpdateTravelStatus();
        UpdateModeButtonVisuals();

        travelButton.interactable = ShipMaterialBank.instance.thrustiumBanked >= requiredThrustium && !droneManager.IsDeployed;
    }

    private void UpdateModeButtonVisuals()
    {
        bool droneDeployed = droneManager != null && droneManager.IsDeployed;

        Color targetColor = droneDeployed ? modeUnavailableColor : modeAvailableColor;

        if(fuelModePanel != null) fuelModePanel.color = targetColor;

        if(satModePanel != null) satModePanel.color = targetColor;

        if(anyModePanel != null) anyModePanel.color = targetColor;
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
        droneManager.ToggleDeployment();
    }

    public void OnClick_AnyMode()
    {
        droneManager.SetModeAny();
        ResetButtonText();
        anyModeTXT.text = "SET";
    }

    public void OnClick_FuelMode()
    {
        droneManager.SetModeFuelium();
        ResetButtonText();
        fuelModeTXT.text = "SET";

    }
    public void OnClick_SatMode()
    {
        droneManager.SetModeSatonium();
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



   IEnumerator ValueUpdateText()
    {
        UpdateTravelStatus();
        TravelStatusBankedTHR.text = $"{ShipMaterialBank.instance.thrustiumBanked:0} / {requiredThrustium:0}";
        yield return new WaitForSeconds(10);
        StartCoroutine(ValueUpdateText());
    }

    void UpdateTravelStatus()
    {
        if(ShipMaterialBank.instance == null || DroneManager.instance == null)
        {
            SetTravelStatus("Travel Status: SYS ERROR", Color.red);
            return;
        }

        float thrustiumBanked = ShipMaterialBank.instance.thrustiumBanked;

        if(droneManager.IsDeployed)
        {
            SetTravelStatus("Travel Status: DRONE AWAY", Color.red);
            return;
        }

        if(thrustiumBanked < requiredThrustium)
        {
            SetTravelStatus("Travel Status: LOW $THR", Color.red);
            return;
        }

        SetTravelStatus("Travel Status: READY", Color.green);
    }

    private bool IsDroneDeployed(string status)
    {
        return status == "Returning" ||
                status == "Out of Power" ||
                status == "Mining" ||
                status == "En-Route";
    }

    private void SetTravelStatus(string message, Color color)
    {
        travelStatusTXT.text = message;
        travelStatusTXT.color = color;
    }

    void DisplayTextValues()
    {
        statusValue.text = droneManager.StatusText;
        sendDroneButtonTXT.text = droneManager.ButtonText;
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
