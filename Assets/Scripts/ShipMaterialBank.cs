using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMaterialBank : MonoBehaviour
{
    public static ShipMaterialBank instance;

    public float satoniumBanked;
    public float thrustiumBanked;
    public float fueliumBanked;

    private void Awake()
    {
        instance = this;
    }
}
