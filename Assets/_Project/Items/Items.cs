using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Items/ItemsObjects")]
public class Items : ScriptableObject
{
    public string itemName;
    public bool food;
    public float satiation;
    public int amountLeft;
    public float flashlightCharge;
    public float fatigueReduction;
}
