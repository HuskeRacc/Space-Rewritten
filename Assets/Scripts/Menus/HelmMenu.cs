using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelmMenu : MonoBehaviour
{

    [SerializeField] PlayerMovement player;
    [SerializeField] DroneManager droneManager;

    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI batteryTXT;

    [SerializeField] TextMeshProUGUI modeValue;

    [SerializeField] TextMeshProUGUI satoniumValue;
    [SerializeField] TextMeshProUGUI thrustiumValue;
    [SerializeField] TextMeshProUGUI fueliumValue;

    [SerializeField] TextMeshProUGUI satoniumBankedValue;
    [SerializeField] TextMeshProUGUI thrustiumBankedValue;
    [SerializeField] TextMeshProUGUI fueliumBankedValue;

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

    private void Update()
    {
        DisplayTextValues();
        DisplayDroneValues();
        DisplayBankedValues();
    }

    void DisplayTextValues()
    {
        statusText.text = droneManager.droneStatus;
        buttonText.text = droneManager.buttonStatus;
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
}
