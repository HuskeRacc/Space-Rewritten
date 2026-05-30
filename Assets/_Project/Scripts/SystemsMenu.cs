using TMPro;
using UnityEngine;

public class SystemsMenu : MonoBehaviour
{
    [SerializeField] ShipSystems systems;
    [SerializeField] PowerGenerator power;
    [SerializeField] OxygenGenerator oxygen;

    [SerializeField] TextMeshProUGUI fuelValue;
    [SerializeField] TextMeshProUGUI oxyValue;
    [SerializeField] TextMeshProUGUI batteryValue;

    [SerializeField] GameObject systemsMenu;

    //Add visual sliders?

    public void OnClick_CloseMenu()
    {
        systemsMenu.SetActive(false);
        PlayerMovement.instance.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        ApplyTextValues();
        ApplyTextColors();
        //UpgradedSystemsText();
    }

    void ApplyTextValues()
    {
        fuelValue.text = systems.fuel.ToString("F1");
        oxyValue.text = systems.shipOxygen.ToString("F1");
        batteryValue.text = ShipSystems.instance.shipBattery.ToString("F2");
    }

    public void UpgradedSystemsText()
    {
        if (power.powerGeneratorAvailable)
        {
            fuelValue.text += " " + power.powerGeneratorActive.ToString();
            oxyValue.text += " " + oxygen.o2GeneratorActive.ToString();
        }
        else
        {
            fuelValue.text = "Error";
            oxyValue.text = "Error";
        }
    }

    void ApplyTextColors()
    {
        if (ShipSystems.instance.solarsActive)
            batteryValue.color = Color.green;
        else
            batteryValue.color = Color.red;

        if (power.powerGeneratorActive)
            fuelValue.color = Color.green;

        else
            fuelValue.color = Color.red;

        if (oxygen.o2GeneratorActive)
            oxyValue.color = Color.green;

        else
            oxyValue.color = Color.red;
    }
}
