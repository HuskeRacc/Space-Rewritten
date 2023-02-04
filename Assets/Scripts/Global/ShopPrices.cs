using System.Collections;
using UnityEngine;

public class ShopPrices : MonoBehaviour
{
    [Header("RNG Variables")]
    [SerializeField] float fuelRNGLow = 300;
    [SerializeField] float fuelRNGHi = 1500;
    [SerializeField] float mreRNGLow = 400;
    [SerializeField] float mreRNGHi = 800;
    [SerializeField] float donutRNGLow = 200;
    [SerializeField] float donutRNGHi = 400;

    [Header("Current Prices")]
    public float fuelPrice;
    public float upgradeOnePrice;

    public float mrePrice;
    public float donutPrice;

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
    }
}
