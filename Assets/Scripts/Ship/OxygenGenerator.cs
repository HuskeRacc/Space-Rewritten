using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OxygenGenerator : Interactable
{
    public bool o2GeneratorActive;
    public bool o2GeneratorAvailable;

    [SerializeField] ShipSystems ship;

    [SerializeField] TextMeshProUGUI statusDisplayTXT;

    public override void OnFocus()
    {
    }

    public override void OnInteract()
    {
        Toggleo2Generator();
    }

    public override void OnLoseFocus()
    {
    }

    private void Update()
    {
        HandleOxygen();

        if (o2GeneratorActive)
        {
            statusDisplayTXT.text = "Running.";
            statusDisplayTXT.color = Color.green;
        }
        else
        {
            statusDisplayTXT.text = "Inactive.";
            statusDisplayTXT.color = Color.red;
        }
    }

    public void Toggleo2Generator()
    {
        if (o2GeneratorAvailable)
            o2GeneratorActive = !o2GeneratorActive;
    }

    void HandleOxygen()
    {
        if (o2GeneratorActive)
        {
            ship.shipOxygen += Time.deltaTime;
        }
    }
}
