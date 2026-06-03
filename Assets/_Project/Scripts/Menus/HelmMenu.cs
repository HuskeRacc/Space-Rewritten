using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelmMenu : MonoBehaviour
{
    public static HelmMenu instance;

    [Header("References")]
    [SerializeField] private PlayerMovement player;
    [SerializeField] private DroneManager droneManager;
    [SerializeField] private GameObject helmMenu;

    [Header("Drone Status UI")]
    [SerializeField] private TextMeshProUGUI statusValue;
    [SerializeField] private TextMeshProUGUI sendDroneButtonTXT;
    [SerializeField] private TextMeshProUGUI batteryValue;
    [SerializeField] private TextMeshProUGUI travelTimeLeftValue;

    [Header("Drone Storage UI")]
    [SerializeField] private TextMeshProUGUI satoniumValue;
    [SerializeField] private TextMeshProUGUI thrustiumValue;
    [SerializeField] private TextMeshProUGUI fueliumValue;

    [Header("Ship Storage UI")]
    [SerializeField] private TextMeshProUGUI satBankedValue;
    [SerializeField] private TextMeshProUGUI fuelBankedValue;
    [SerializeField] private TextMeshProUGUI thruBankedValue;

    [Header("Travel UI")]
    [SerializeField] private TextMeshProUGUI travelStatusTXT;
    [SerializeField] private TextMeshProUGUI travelStatusBankedTHR;
    [SerializeField] private Button travelButton;
    [SerializeField] private float requiredThrustium = 250f;
    [SerializeField] private int travelSceneIndex = 2;

    [Header("Mode Button Text")]
    [SerializeField] private TextMeshProUGUI fuelModeTXT;
    [SerializeField] private TextMeshProUGUI satModeTXT;
    [SerializeField] private TextMeshProUGUI anyModeTXT;

    [Header("Mode Button Graphics")]
    [SerializeField] private Image fuelModePanel;
    [SerializeField] private Image satModePanel;
    [SerializeField] private Image anyModePanel;
    [SerializeField] private Color modeAvailableColor = Color.green;
    [SerializeField] private Color modeUnavailableColor = Color.red;

    private float timeToTravelCalculated;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateModeButtonText();
    }

    private void Update()
    {
        UpdateDroneStatusUI();
        UpdateDroneStorageUI();
        UpdateShipStorageUI();
        UpdateTravelUI();
        UpdateModeButtonVisuals();
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
        if (droneManager == null)
            return;

        droneManager.ToggleDeployment();
    }

    public void OnClick_AnyMode()
    {
        if (droneManager == null)
            return;

        droneManager.SetModeAny();
        UpdateModeButtonText();
    }

    public void OnClick_FuelMode()
    {
        if (droneManager == null)
            return;

        droneManager.SetModeFuelium();
        UpdateModeButtonText();
    }

    public void OnClick_SatMode()
    {
        if (droneManager == null)
            return;

        droneManager.SetModeSatonium();
        UpdateModeButtonText();
    }

    public void OnClick_Travel()
    {
        if (!CanTravel())
            return;

        SceneManager.LoadScene(travelSceneIndex);
    }

    public void DisplayTravelTime(float timeToTravel)
    {
        timeToTravelCalculated = timeToTravel;

        travelTimeLeftValue.text = timeToTravelCalculated.ToString("F0");
        travelTimeLeftValue.gameObject.SetActive(true);

        CancelInvoke(nameof(CalculateTravelTimeLeft));
        InvokeRepeating(nameof(CalculateTravelTimeLeft), 0, 1);
    }

    private void UpdateDroneStatusUI()
    {
        if (droneManager == null)
            return;

        statusValue.text = droneManager.StatusText;
        sendDroneButtonTXT.text = droneManager.ButtonText;
        batteryValue.text = $"{droneManager.battery:0}%";
    }

    private void UpdateDroneStorageUI()
    {
        if (droneManager == null)
            return;

        satoniumValue.text = droneManager.satoniumAmount.ToString("F2");
        thrustiumValue.text = droneManager.thrustiumAmount.ToString("F2");
        fueliumValue.text = droneManager.fueliumAmount.ToString("F2");
    }

    private void UpdateShipStorageUI()
    {
        if (ShipMaterialBank.instance == null)
            return;

        if (satBankedValue != null)
            satBankedValue.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");

        if (thruBankedValue != null)
            thruBankedValue.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");

        if (fuelBankedValue != null)
            fuelBankedValue.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
    }

    private void UpdateTravelUI()
    {
        UpdateTravelRequirementText();
        UpdateTravelStatus();

        if (travelButton != null)
            travelButton.interactable = CanTravel();
    }

    private void UpdateTravelRequirementText()
    {
        if (travelStatusBankedTHR == null)
            return;

        if (ShipMaterialBank.instance == null)
        {
            travelStatusBankedTHR.text = $"0 / {requiredThrustium:0}";
            return;
        }

        travelStatusBankedTHR.text = $"{ShipMaterialBank.instance.thrustiumBanked:0} / {requiredThrustium:0}";
    }

    private void UpdateTravelStatus()
    {
        if (ShipMaterialBank.instance == null || droneManager == null)
        {
            SetTravelStatus("SYS ERROR", Color.red);
            return;
        }

        if (droneManager.IsDeployed)
        {
            SetTravelStatus("DRONE AWAY", Color.red);
            return;
        }

        if (ShipMaterialBank.instance.thrustiumBanked < requiredThrustium)
        {
            SetTravelStatus("LOW THRUSTIUM", Color.red);
            return;
        }

        SetTravelStatus("READY", Color.green);
    }

    private bool CanTravel()
    {
        if (ShipMaterialBank.instance == null || droneManager == null)
            return false;

        if (droneManager.IsDeployed)
            return false;

        return ShipMaterialBank.instance.thrustiumBanked >= requiredThrustium;
    }

    private void SetTravelStatus(string message, Color color)
    {
        if (travelStatusTXT == null)
            return;

        travelStatusTXT.text = message;
        travelStatusTXT.color = color;
    }

    private void UpdateModeButtonText()
    {
        ResetModeButtonText();

        if (droneManager == null)
            return;

        switch(droneManager.Mode)
        {
            case DroneManager.DroneMiningMode.Any:
                anyModeTXT.text = "SET";
                break;

            case DroneManager.DroneMiningMode.Fuelium:
                fuelModeTXT.text = "SET";
                break;

            case DroneManager.DroneMiningMode.Satonium:
                satModeTXT.text = "SET";
                break;
        }
    }

    private void UpdateModeButtonVisuals()
    {
        bool droneDeployed = droneManager != null && droneManager.IsDeployed;
        Color targetColor = droneDeployed ? modeUnavailableColor : modeAvailableColor;

        if (fuelModePanel != null)
            fuelModePanel.color = targetColor;

        if (satModePanel != null)
            satModePanel.color = targetColor;

        if (anyModePanel != null)
            anyModePanel.color = targetColor;
    }

    private void ResetModeButtonText()
    {
        satModeTXT.text = "SET MODE";
        fuelModeTXT.text = "SET MODE";
        anyModeTXT.text = "SET MODE";
    }

    private void CalculateTravelTimeLeft()
    {
        if (timeToTravelCalculated <= 0)
        {
            travelTimeLeftValue.gameObject.SetActive(false);
            CancelInvoke(nameof(CalculateTravelTimeLeft));
            return;
        }

        travelTimeLeftValue.text = timeToTravelCalculated.ToString("F0");
        timeToTravelCalculated--;
    }
}