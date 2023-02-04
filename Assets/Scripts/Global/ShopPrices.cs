using System.Collections;
using UnityEngine;

public class ShopPrices : MonoBehaviour
{
    [Header("RNG Variables")]
    [SerializeField] float fuelRNGLow = 300;
    [SerializeField] float fuelRNGHi = 1500;

    [Header("Current Prices")]
    public float fuelPrice;
    public float upgradeOnePrice;

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
        InvokeRepeating("FuelPriceVary", 0, 300f);
    }

    void FuelPriceVary()
    {
        fuelPrice = Random.Range(fuelRNGLow, fuelRNGHi);
    }
}
