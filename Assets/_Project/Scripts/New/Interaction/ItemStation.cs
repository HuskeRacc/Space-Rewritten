using UnityEngine;

public class ItemStation : Interactable
{
    [Header("Station Configuration")]
    public ItemData itemData;

    [Header("Dynamic Stock Tracking")]
    public int currentUses = 0;       // The current amount of food/batteries in this specific pile
    public int maxCapacity = 15;      // Optional limit so they can't craft 9,000 cans onto one table

    [Header("Visuals")]
    [Tooltip("Drag the individual 3D cans/items inside this station here. They will enable/disable based on stock.")]
    public GameObject[] itemMeshes;

    private void Start()
    {
        // Update the visuals as soon as the game starts
        UpdateVisuals();
    }

    public override void OnFocus()
    {
        if (itemData != null)
        {
            if (currentUses > 0)
                Debug.Log($"Looking at: {itemData.itemName} ({currentUses} available)");
            else
                Debug.Log($"{itemData.itemName} station is empty. Craft more!");
        }
    }

    public override void OnInteract()
    {
        // Prevent interaction if the station is completely empty
        if (itemData == null || currentUses <= 0)
        {
            Debug.Log("Nothing left to use here!");
            // Optional: Play a negative "buzz" or empty click sound here
            return;
        }

        // 1. Apply the stat effects from the ScriptableObject
        itemData.UseItem();

        // 2. Reduce the local station stock
        currentUses--;

        // 3. Update the 3D meshes to reflect the new count
        UpdateVisuals();
    }

    public override void OnLoseFocus()
    {
        // Hide UI tooltips
    }

    // --- CRAFTING INTEGRATION ---
    // Your crafting or shop scripts will call this method to refill the station!
    public void AddStock(int amountAdded)
    {
        currentUses += amountAdded;

        // Clamp to max capacity so the logic doesn't exceed your visual meshes
        if (currentUses > maxCapacity)
        {
            currentUses = maxCapacity;
        }

        UpdateVisuals();
        Debug.Log($"Added {amountAdded} to {itemData.itemName} station. Total: {currentUses}");
    }

    private void UpdateVisuals()
    {
        // Skip if you haven't assigned the meshes in the inspector
        if (itemMeshes == null || itemMeshes.Length == 0) return;

        // Loop through all assigned meshes and turn them on/off based on currentUses
        for (int i = 0; i < itemMeshes.Length; i++)
        {
            // If the mesh index is lower than our current stock, turn it on. Otherwise, off.
            // Example: If currentUses is 3, meshes at index 0, 1, and 2 are activated. 
            // The rest vanish.
            itemMeshes[i].SetActive(i < currentUses);
        }
    }
}