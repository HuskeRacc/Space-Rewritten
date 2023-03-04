using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMaterialBank : MonoBehaviour, ISaveable
{
    public static ShipMaterialBank instance;

    public float satoniumBanked;
    public float thrustiumBanked;
    public float fueliumBanked;

    public object CaptureState()
    {
        return new SaveData
        {
            savedSatoniumBanked= satoniumBanked,
            savedFueliumBanked= fueliumBanked,
            savedThrustiumBanked= thrustiumBanked
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData)state;

        satoniumBanked = saveData.savedSatoniumBanked;
        fueliumBanked = saveData.savedFueliumBanked;
        thrustiumBanked = saveData.savedThrustiumBanked;
    }

    [Serializable]
    private struct SaveData
    {
        public float savedSatoniumBanked;
        public float savedThrustiumBanked;
        public float savedFueliumBanked;
    }

    private void Awake()
    {
        instance = this;
    }


}
