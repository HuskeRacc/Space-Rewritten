using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSystem : MonoBehaviour
{
    [SerializeField] float OldFuelPrice;
    [SerializeField] float OldMREPrice;
    [SerializeField] float OldDonutPrice;
    [SerializeField] float OldBatteryPrice;

    [SerializeField] List<GameObject> stockIndicators;

    private void Awake()
    {
        stockIndicators.AddRange(GameObject.FindGameObjectsWithTag("stockindicator"));

        for (int i = 0; i < stockIndicators.Count; i++)
        {
            stockIndicators[i].SetActive(false);
        }
    }

    private void Start()
    {
        OldFuelPrice = ShopPrices.instance.fuelPrice;
        OldMREPrice = ShopPrices.instance.mrePrice;
        OldDonutPrice = ShopPrices.instance.donutPrice;
        OldBatteryPrice = ShopPrices.instance.batteryPrice;
        InvokeRepeating(nameof(DisplayPriceDifferences), 0f, ShopPrices.instance.priceVaryRate);
    }

    void DisplayPriceDifferences()
    {
        //Fuel
        if (OldFuelPrice < ShopPrices.instance.fuelPrice)
        {
            stockIndicators[1].SetActive(true);
            stockIndicators[0].SetActive(false);
            OldFuelPrice = ShopPrices.instance.fuelPrice;
        }
        else if (OldFuelPrice > ShopPrices.instance.fuelPrice)
        {
            stockIndicators[1].SetActive(false);
            stockIndicators[0].SetActive(true);
            OldFuelPrice = ShopPrices.instance.fuelPrice;
        }

        //MRE
        if (OldMREPrice < ShopPrices.instance.mrePrice)
        {
            stockIndicators[3].SetActive(true);
            stockIndicators[2].SetActive(false);
            OldMREPrice = ShopPrices.instance.mrePrice;
        }
        else if (OldMREPrice > ShopPrices.instance.mrePrice)
        {
            stockIndicators[3].SetActive(true);
            stockIndicators[2].SetActive(false);
            OldMREPrice = ShopPrices.instance.mrePrice;
        }

        //Donut
        if (OldDonutPrice < ShopPrices.instance.donutPrice)
        {
            stockIndicators[5].SetActive(true);
            stockIndicators[4].SetActive(false);
            OldDonutPrice = ShopPrices.instance.donutPrice;
        }
        else if (OldDonutPrice > ShopPrices.instance.donutPrice)
        {
            stockIndicators[5].SetActive(false);
            stockIndicators[4].SetActive(true);
            OldDonutPrice = ShopPrices.instance.donutPrice;
        }

        //Battery
        if (OldBatteryPrice < ShopPrices.instance.batteryPrice)
        {
            stockIndicators[7].SetActive(true);
            stockIndicators[6].SetActive(false);
            OldBatteryPrice = ShopPrices.instance.batteryPrice;
        }
        else if (OldBatteryPrice > ShopPrices.instance.batteryPrice)
        {
            stockIndicators[7].SetActive(false);
            stockIndicators[6].SetActive(true);
            OldBatteryPrice = ShopPrices.instance.batteryPrice;
        }
    }
}
