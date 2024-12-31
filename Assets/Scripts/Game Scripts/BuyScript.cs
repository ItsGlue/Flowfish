using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyScript : MonoBehaviour
{
    public int cost; 
    public string itemName; 

    //called on button press
    public void onClick() {
        if (cost <= MoneyManager.instance.GetMoney()) {
            MoneyManager.instance.SubtractMoney(cost);
            InventoryManager.instance.AddItem(itemName);
        }
    }
}
