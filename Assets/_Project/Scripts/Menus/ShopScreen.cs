using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fuelPriceTXT;

    [SerializeField] TextMeshProUGUI mrePriceTXT;
    [SerializeField] TextMeshProUGUI donutPriceTXT;
    [SerializeField] TextMeshProUGUI batteryPriceTXT;
    [SerializeField] TextMeshProUGUI coffeePriceTXT;

    [SerializeField] TextMeshProUGUI satoniumBankedValue;
    [SerializeField] TextMeshProUGUI thrustiumBankedValue;
    [SerializeField] TextMeshProUGUI fueliumBankedValue;

    private void Update()
    {
        DisplayPrices();
        DisplayBankedValues();
    }


     void DisplayPrices()
    {
        fuelPriceTXT.text = "$Fu " + ShopPrices.instance.fuelPrice.ToString("F1");
        mrePriceTXT.text = "$Sa " + ShopPrices.instance.mrePrice.ToString("F1");
        donutPriceTXT.text = "$Sa " + ShopPrices.instance.donutPrice.ToString("F1");
        batteryPriceTXT.text = "$Fu " + ShopPrices.instance.batteryPrice.ToString("F1");
        coffeePriceTXT.text = "$Sa " + ShopPrices.instance.coffeePrice.ToString("F1");
    }

    void DisplayBankedValues()
    {
        satoniumBankedValue.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");
        thrustiumBankedValue.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");
        fueliumBankedValue.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
    }
}
