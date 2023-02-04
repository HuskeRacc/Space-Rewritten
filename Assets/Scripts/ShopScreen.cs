using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] TextMeshProUGUI currencyValue;

    [SerializeField] TextMeshProUGUI fuelPriceTXT;

    [SerializeField] TextMeshProUGUI mrePriceTXT;
    [SerializeField] TextMeshProUGUI donutPriceTXT;

    private void Update()
    {
        currencyValue.text = "$" + playerCurrency.currency.ToString("F1");
        fuelPriceTXT.text = ShopPrices.instance.fuelPrice.ToString("F2");
        mrePriceTXT.text = ShopPrices.instance.mrePrice.ToString("F2");
        mrePriceTXT.text = ShopPrices.instance.donutPrice.ToString("F2");
    }
}
