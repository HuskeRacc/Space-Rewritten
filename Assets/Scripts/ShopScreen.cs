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
        fuelPriceTXT.text = "$Fuelium " + ShopPrices.instance.fuelPrice.ToString("F2");
        mrePriceTXT.text = "$Satonium " + ShopPrices.instance.mrePrice.ToString("F2");
        donutPriceTXT.text = "$Satonium " + ShopPrices.instance.donutPrice.ToString("F2");
        batteryPriceTXT.text = "$Fuelium " + ShopPrices.instance.batteryPrice.ToString("F2");
    }

    void DisplayBankedValues()
    {
        satoniumBankedValue.text = ShipMaterialBank.instance.satoniumBanked.ToString("F2");
        thrustiumBankedValue.text = ShipMaterialBank.instance.thrustiumBanked.ToString("F2");
        fueliumBankedValue.text = ShipMaterialBank.instance.fueliumBanked.ToString("F2");
    }
}
