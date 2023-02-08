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

    [Header("Current Prices")]
    public float fuelPrice;
    public float upgradeOnePrice;

    public float mrePrice;
    public float donutPrice;
    public float batteryPrice;

    [Header("Upgrades")]
    public float[] upgradePrices;
    
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
        InvokeRepeating("PriceVaries", 0, 300f);
    }

    void PriceVaries()
    {
        fuelPrice = Random.Range(fuelRNGLow, fuelRNGHi);
        mrePrice = Random.Range(mreRNGLow, mreRNGHi);
        donutPrice = Random.Range(donutRNGLow, donutRNGHi);
        batteryPrice = Random.Range(batteryRNGLow, batteryRNGHi);
    }
}
