using UnityEngine;

public class WorldItem : Interactable
{
    [Header("Item Configuration")]
    public ItemData itemData;

    // This tracks the physical object's state in the world
    private int currentUses;

    void Start()
    {
        if (itemData != null)
        {
            // Initialize the uses based on the item's blueprint
            currentUses = itemData.maxUses;
        }
    }

    public override void OnFocus()
    {
        if (itemData != null)
        {
            Debug.Log($"Looking at: {itemData.itemName} ({currentUses}/{itemData.maxUses} uses left)");
        }
    }

    public override void OnInteract()
    {
        if (itemData == null) return;

        // 1. Apply the effect
        itemData.UseItem();

        // 2. Reduce the remaining uses
        currentUses--;

        // 3. Destroy the 3D object if it's empty
        if (currentUses <= 0)
        {
            Debug.Log($"{itemData.itemName} is fully consumed!");
            Destroy(gameObject);
        }
    }

    public override void OnLoseFocus()
    {
        // Hide UI tooltips here
    }
}