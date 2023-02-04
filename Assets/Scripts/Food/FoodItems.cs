using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food Items", menuName = "Food/FoodObjects")]
public class FoodItems : ScriptableObject
{
    public string nameOfFood;
    public float satiation;
    public int amountLeft;
}
