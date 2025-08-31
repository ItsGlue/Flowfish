using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    
    void Update()
    {
        textMeshPro.text = "Money: $" + MoneyManager.instance.GetMoney().ToString();
    }
    
}
