using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DateText : MonoBehaviour
{
    public TMP_Text textMeshPro;
    public DateTime dateTime;


    void Start() 
    {
        textMeshPro.text = DateManager.Instance.getSelectedDate().ToString("D");
    }
    
}
