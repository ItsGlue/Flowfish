using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Money: $" + MoneyManager.instance.GetMoney().ToString();
    }
    
}
