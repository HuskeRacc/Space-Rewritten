using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelmMenu : MonoBehaviour
{

    public static HelmMenu instance;

    [SerializeField] PlayerMovement player;
    [SerializeField] DroneManager droneManager;

    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI buttonTXT;
    [SerializeField] TextMeshProUGUI batteryTXT;
    public TextMeshProUGUI travelTimeLeftValue;
    [SerializeField] float timeToTravelCalculated;

    [SerializeField] TextMeshProUGUI modeValue;

    [SerializeField] TextMeshProUGUI satoniumValue;
    [SerializeField] TextMeshProUGUI thrustiumValue;
    [SerializeField] TextMeshProUGUI fueliumValue;

    [SerializeField] TextMeshProUGUI satoniumBankedValue;
    [SerializeField] TextMeshProUGUI thrustiumBankedValue;
    [SerializeField] TextMeshProUGUI fueliumBankedValue;

    [SerializeField] Button travelButton;

    [SerializeField] GameObject helmMenu;

    private void Awake()
    {
        instance = this;
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

    public void OnClick_ResetMode0()
    {
        DroneManager.instance.mode = 0;
        modeValue.text = "0";
    }

    public void OnClick_SetMode1()
    {
        DroneManager.instance.mode = 1;
        modeValue.text = "1";
    }
    public void OnClick_SetMode2()
    {
        DroneManager.instance.mode = 2;
        modeValue.text = "2";
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
        DisplayBankedValues();

        if (ShipMaterialBank.instance.thrustiumBanked >= 250)
        {
            travelButton.interactable = true;
        }
        else
        {
            travelButton.interactable = false;
        }
    }

    void DisplayTextValues()
    {
        statusText.text = droneManager.droneStatus;
        buttonTXT.text = droneManager.buttonStatus;
        batteryTXT.text = "Battery Power: " + droneManager.battery.ToString("F0") + "%";
    }

    void DisplayDroneValues()
    {
        satoniumValue.text = DroneManager.instance.satoniumAmount.ToString("F2");
        thrustiumValue.text = DroneManager.instance.thrustiumAmount.ToString("F2");
        fueliumValue.text = DroneManager.instance.fueliumAmount.ToString("F2");
    }

    void DisplayBankedValues()
    {
        satoniumBankedValue.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");
        thrustiumBankedValue.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");
        fueliumBankedValue.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
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
