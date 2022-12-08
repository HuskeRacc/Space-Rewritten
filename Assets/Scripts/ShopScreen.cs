using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] PlayerCurrency playerCurrency;
    [SerializeField] TextMeshProUGUI currencyValue;

    [SerializeField] TextMeshProUGUI fuelPriceTXT;

    // Placeholder Fuel Price
    public float fuelPrice = 500f;

    private void Update()
    {
        currencyValue.text = "$" + playerCurrency.currency.ToString("F1");
        fuelPriceTXT.text = fuelPrice.ToString("F0");
    }
}
