using UnityEngine;

[CreateAssetMenu(fileName = "New Battery", menuName = "Items/Battery")]
public class BatteryItemData : ItemData
{
    [Header("Battery Stats (Per Use)")]
    public float chargeAmount = 100f;

    public override void UseItem()
    {
        Debug.Log($"Recharged flashlight by {chargeAmount}.");
        // PlayerMovement.instance.flashlightBattery += chargeAmount;
    }
}