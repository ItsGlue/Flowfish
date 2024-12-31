using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdater : MonoBehaviour
{

    public TextMeshProUGUI textMeshPro;
    public string trackedItem; 
    

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = InventoryManager.instance.GetItemQuantity(trackedItem).ToString();
    }
}
