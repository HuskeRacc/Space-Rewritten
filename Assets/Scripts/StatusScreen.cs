using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusScreen : MonoBehaviour
{
    [SerializeField] ShipSystems systems;
    [SerializeField] PowerGenerator power;
    [SerializeField] OxygenGenerator oxygen;

    [SerializeField] TextMeshProUGUI fuelValue;
    [SerializeField] TextMeshProUGUI oxyValue;

    private void Update()
    {
        fuelValue.text = systems.fuel.ToString("F0");
        oxyValue.text = systems.shipOxygen.ToString("F0");
        //Add visual sliders

        if(power.powerGeneratorAvailable)
        {
            fuelValue.text = fuelValue.text + " : " + power.powerGeneratorActive.ToString();
            oxyValue.text = oxyValue.text + " : " + oxygen.o2GeneratorActive.ToString();
        }
        else
        {
            fuelValue.text = "Error";
            oxyValue.text = "Error";
        }


        if (power.powerGeneratorActive == true)
        {
            fuelValue.color = Color.green;
        }
        else if(power.powerGeneratorActive == false)
        {
            fuelValue.color = Color.red;
        }

        if (oxygen.o2GeneratorActive == true)
        {
            oxyValue.color = Color.green;
        }
        else if (oxygen.o2GeneratorActive == false)
        {
            oxyValue.color = Color.red;
        }

    }
}
