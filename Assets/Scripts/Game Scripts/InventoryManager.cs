using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
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
    public void RemoveItem(string itemName, int quantity = 1)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] -= quantity;
            if (items[itemName] <= 0)
            {
                items.Remove(itemName); 
            }
        }
    }
    public void SetItemQuantity(string itemName, int quantity)
    {
        if (quantity > 0)
        {
            items[itemName] = quantity;
        }
        else
        {
            items.Remove(itemName);
        }
    }

    public int GetItemQuantity(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        return 0;
    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }
    public void ClearInventory()
    {
        items.Clear();
    }
}
