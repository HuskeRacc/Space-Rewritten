using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicCrosshair : MonoBehaviour
{
    public static DynamicCrosshair instance;
    [SerializeField] Image crosshair;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SmoothCrosshairDisable();
    }

    public void SmoothCrosshairEnable()
    {
        crosshair.enabled = true;
    }

    public void SmoothCrosshairDisable()
    {
        crosshair.enabled = false;
    }
}
