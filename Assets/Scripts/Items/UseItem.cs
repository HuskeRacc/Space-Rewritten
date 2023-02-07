using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : Interactable
{
    [SerializeField] private Items items;
    [SerializeField] int amountLeft = 100;

    public override void OnFocus()
    {
        if(items.food)
        {
            if (items.name == "MRE")
            {
                if (amountLeft == 100) // 100 is default value.
                    amountLeft = items.amountLeft;
            }

            if (items.name == "Donut")
            {
                if (amountLeft == 100) // 100 is default value.
                    amountLeft = items.amountLeft;
            }
        }
    }

    public override void OnInteract()
    {
        if(items.food)
        {
            if (items.name == "MRE")
            {
                if (amountLeft > 1)
                {
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    amountLeft--;
                }
                else if (amountLeft <= 1)
                {
                    Debug.Log("No charges left");
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    DynamicCrosshair.instance.SmoothCrosshairDisable();
                    Destroy(this.gameObject);
                }
            }

            if (items.name == "Donut")
            {
                if (amountLeft > 1)
                {
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    amountLeft--;
                }
                else if (amountLeft <= 1)
                {
                    Debug.Log("No charges left");
                    PlayerNeeds.instance.HungerIncrease(items.satiation);
                    DynamicCrosshair.instance.SmoothCrosshairDisable();
                    Destroy(this.gameObject);
                }
            }
        }

        if(items.name == "Battery")
        {
            PlayerMovement.instance.flashlightBattery = 100;
            Destroy(this.gameObject);
        }
    }

    public override void OnLoseFocus()
    {
        amountLeft = 100;
    }
}
