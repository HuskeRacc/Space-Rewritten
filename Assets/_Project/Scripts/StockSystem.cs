using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockSystem : MonoBehaviour
{
    [SerializeField] float OldFuelPrice;
    [SerializeField] float OldMREPrice;
    [SerializeField] float OldDonutPrice;
    [SerializeField] float OldBatteryPrice;
    [SerializeField] float OldCoffeePrice;

    [SerializeField] List<GameObject> stockIndicators;

    private void Awake()
    {

    }

    private void Start()
    {
        for (int i = 0; i < stockIndicators.Count; i++)
        {
            stockIndicators[i].SetActive(false);
        }
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
            stockIndicators[0].SetActive(true);
            stockIndicators[1].SetActive(false);
            OldFuelPrice = ShopPrices.instance.fuelPrice;
        }
        else if (OldFuelPrice > ShopPrices.instance.fuelPrice)
        {
            stockIndicators[0].SetActive(false);
            stockIndicators[1].SetActive(true);
            OldFuelPrice = ShopPrices.instance.fuelPrice;
        }

        //MRE
        if (OldMREPrice < ShopPrices.instance.mrePrice)
        {
            stockIndicators[2].SetActive(true);
            stockIndicators[3].SetActive(false);
            OldMREPrice = ShopPrices.instance.mrePrice;
        }
        else if (OldMREPrice > ShopPrices.instance.mrePrice)
        {
            stockIndicators[2].SetActive(true);
            stockIndicators[3].SetActive(false);
            OldMREPrice = ShopPrices.instance.mrePrice;
        }

        //Donut
        if (OldDonutPrice < ShopPrices.instance.donutPrice)
        {
            stockIndicators[4].SetActive(true);
            stockIndicators[5].SetActive(false);
            OldDonutPrice = ShopPrices.instance.donutPrice;
        }
        else if (OldDonutPrice > ShopPrices.instance.donutPrice)
        {
            stockIndicators[4].SetActive(false);
            stockIndicators[5].SetActive(true);
            OldDonutPrice = ShopPrices.instance.donutPrice;
        }

        //Battery
        if (OldBatteryPrice < ShopPrices.instance.batteryPrice)
        {
            stockIndicators[6].SetActive(true);
            stockIndicators[7].SetActive(false);
            OldBatteryPrice = ShopPrices.instance.batteryPrice;
        }
        else if (OldBatteryPrice > ShopPrices.instance.batteryPrice)
        {
            stockIndicators[6].SetActive(false);
            stockIndicators[7].SetActive(true);
            OldBatteryPrice = ShopPrices.instance.batteryPrice;
        }

        //Coffee
        if (OldCoffeePrice < ShopPrices.instance.coffeePrice)
        {
            stockIndicators[8].SetActive(true);
            stockIndicators[9].SetActive(false);
            OldBatteryPrice = ShopPrices.instance.coffeePrice;
        }
        else if (OldCoffeePrice > ShopPrices.instance.coffeePrice)
        {
            stockIndicators[8].SetActive(false);
            stockIndicators[9].SetActive(true);
            OldBatteryPrice = ShopPrices.instance.coffeePrice;
        }
    }
}
