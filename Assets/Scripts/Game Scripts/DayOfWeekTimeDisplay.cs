using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DayOfWeekTimeDisplay : MonoBehaviour
{
    [Header("Which day do you want to show?")]
    [SerializeField] private DayOfWeek dayToDisplay = DayOfWeek.Monday;
    
    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (TimeTrackingManager.Instance == null) return;
        float totalSeconds = TimeTrackingManager.Instance.GetTimeForDay(dayToDisplay);
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);
        tmpText.text = $"{hours:00}:{minutes:00}:{seconds:00}";
    }
}
