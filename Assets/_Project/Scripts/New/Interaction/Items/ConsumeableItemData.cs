using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/Consumable")]
public class ConsumableItemData : ItemData
{
    [Header("Consumable Stats (Per Use)")]
    public float satiationRestore;
    public float fatigueRestore;

    public override void UseItem()
    {
        // Check if the PlayerNeeds instance exists to avoid errors
        if (PlayerNeeds.instance != null)
        {
            // If this item provides food (MRE, Donut)
            if (satiationRestore > 0)
            {
                PlayerNeeds.instance.HungerIncrease(satiationRestore);
                Debug.Log($"Ate food: Restored {satiationRestore} hunger.");
            }

            // If this item provides energy (Coffee)
            if (fatigueRestore > 0)
            {
                PlayerNeeds.instance.FatigueIncrease(fatigueRestore);
                Debug.Log($"Drank coffee: Restored {fatigueRestore} fatigue.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerNeeds instance is missing! Cannot apply consumable stats.");
        }
    }
}