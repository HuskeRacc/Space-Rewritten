using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("Base Info")]
    public string itemName;
    [TextArea(2, 4)]
    public string description;

    [Header("Usage")]
    [Tooltip("How many times can this item be interacted with before it is destroyed?")]
    public int maxUses = 1;

    public virtual void UseItem()
    {
        // The base class does nothing on its own, 
        // it just provides the blueprint for the specific items.
    }
}