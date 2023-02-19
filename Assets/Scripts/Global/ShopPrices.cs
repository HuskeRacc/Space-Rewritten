using System.Collections;
using UnityEngine;

public class ShopPrices : MonoBehaviour
{
    [Header("RNG Variables")]
    [SerializeField] float fuelRNGLow = 100;
    [SerializeField] float fuelRNGHi = 300;
    [SerializeField] float mreRNGLow = 50;
    [SerializeField] float mreRNGHi = 250;
    [SerializeField] float donutRNGLow = 25;
    [SerializeField] float donutRNGHi = 200;
    [SerializeField] float batteryRNGHi = 10;
    [SerializeField] float batteryRNGLow = 25;
    [SerializeField] float coffeeRNGLow = 25;
    [SerializeField] float coffeeRNGHi = 200;

    [Header("Current Prices")]
    public float fuelPrice;
    public float upgradeOnePrice;

    public float mrePrice;
    public float donutPrice;
    public float batteryPrice;
    public float coffeePrice;

    [Header("Upgrades")]
    public float[] upgradePrices;

    [Header("Assignables")]
    public bool hasVaried = false;
    public float priceVaryRate = 300f;

    [Header("Repair Prices")]
    public float[] repairPrices;

    [Header("Script checks every 5 seconds")]
    public bool forcePriceVary = false;
    
    public static ShopPrices instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        InvokeRepeating(nameof(PriceVaries), 0, priceVaryRate);
        InvokeRepeating(nameof(ForcePriceVary), 5, 5);
        StartCoroutine(PriceVariedCall());
    }

    void ForcePriceVary()
    {
        if(forcePriceVary == true)
        {
            PriceVaries();
            forcePriceVary = false;
        }
    }

    void PriceVaries()
    {
        fuelPrice = Mathf.Ceil(Random.Range(fuelRNGLow, fuelRNGHi));
        mrePrice = Mathf.Ceil(Random.Range(mreRNGLow, mreRNGHi));
        donutPrice = Mathf.Ceil(Random.Range(donutRNGLow, donutRNGHi));
        batteryPrice = Mathf.Ceil(Random.Range(batteryRNGLow, batteryRNGHi));
        coffeePrice = Mathf.Ceil(Random.Range(coffeeRNGLow, coffeeRNGHi));
        StartCoroutine(PriceVariedCall());
    }

    IEnumerator PriceVariedCall()
    {
        hasVaried = true;
        yield return new WaitForSeconds(1);
        hasVaried = false;
    }
}
