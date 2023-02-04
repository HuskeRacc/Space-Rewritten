using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFood : Interactable
{
    [SerializeField] private FoodItems foodItems;
    [SerializeField] int amountLeft = 100;

    public override void OnFocus()
    {
        if (foodItems.name == "MRE")
        {
            if (amountLeft == 100) // 100 is default value.
                amountLeft = foodItems.amountLeft;
        }

        if (foodItems.name == "Donut")
        {
            if (amountLeft == 100) // 100 is default value.
                amountLeft = foodItems.amountLeft;
        }
    }

    public override void OnInteract()
    {
        if(foodItems.name == "MRE")
        {
            if (amountLeft > 1)
            {
                PlayerNeeds.instance.HungerIncrease(foodItems.satiation);
                amountLeft--;
            }
            else if (amountLeft <= 1)
            {
                Debug.Log("No charges left");
                PlayerNeeds.instance.HungerIncrease(foodItems.satiation);
                DynamicCrosshair.instance.SmoothCrosshairDisable();
                Destroy(this.gameObject);
            }
        }

        if (foodItems.name == "Donut")
        {
            if (amountLeft > 1)
            {
                PlayerNeeds.instance.HungerIncrease(foodItems.satiation);
                amountLeft--;
            }
            else if (amountLeft <= 1)
            {
                Debug.Log("No charges left");
                PlayerNeeds.instance.HungerIncrease(foodItems.satiation);
                DynamicCrosshair.instance.SmoothCrosshairDisable();
                Destroy(this.gameObject);
            }
        }

    }

    public override void OnLoseFocus()
    {
        amountLeft = 100;
    }
}
