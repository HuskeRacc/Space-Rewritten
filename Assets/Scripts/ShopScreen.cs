using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] TextMeshProUGUI currencyValue;

    [SerializeField] TextMeshProUGUI fuelPriceTXT;

    private void Update()
    {
        currencyValue.text = "$" + playerCurrency.currency.ToString("F1");
        fuelPriceTXT.text = ShopPrices.instance.fuelPrice.ToString("F2");
    }
}
