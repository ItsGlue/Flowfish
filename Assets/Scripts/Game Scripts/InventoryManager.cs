using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    // Dictionary to hold item types and their quantities
    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        // Implementing the Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy if another instance exists
        }
    }

    // Add an item to the inventory (increments quantity if the item exists)
    public void AddItem(string itemName, int quantity = 1)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += quantity;
        }
        else
        {
            items[itemName] = quantity;
        }
    }

    // Remove an item from the inventory (decrements quantity or removes item if quantity reaches 0)
    public void RemoveItem(string itemName, int quantity = 1)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] -= quantity;
            if (items[itemName] <= 0)
            {
                items.Remove(itemName); // Remove item if quantity reaches zero
            }
        }
    }

    // Set the quantity of a specific item
    public void SetItemQuantity(string itemName, int quantity)
    {
        if (quantity > 0)
        {
            items[itemName] = quantity;
        }
        else
        {
            items.Remove(itemName); // Remove item if quantity is zero or less
        }
    }

    // Get the quantity of a specific item
    public int GetItemQuantity(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        return 0; // Return 0 if the item doesn't exist
    }

    // Check if an item exists in the inventory
    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }

    // Clear all items in the inventory
    public void ClearInventory()
    {
        items.Clear();
    }
}
