using UnityEngine;
using System;
using System.Collections.Generic;

public class TimeTrackingManager : MonoBehaviour
{
    public static TimeTrackingManager Instance { get; private set; }
    private Dictionary<DayOfWeek, float> timeByDay = new Dictionary<DayOfWeek, float>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddTimeForToday(float timeInSeconds)
    {
        DayOfWeek today = DateTime.Now.DayOfWeek;
        
        if (!timeByDay.ContainsKey(today))
        {
            timeByDay[today] = 0f;
        }
        timeByDay[today] += timeInSeconds;
        Debug.Log($"[TimeTrackingManager] Added {timeInSeconds:F2}s for {today}. Total: {timeByDay[today]:F2}s.");
    }

    public float GetTimeForDay(DayOfWeek day)
    {
        if (timeByDay.ContainsKey(day))
        {
            return timeByDay[day];
        }
        return 0f;
    }
    public float GetTimeForToday()
    {
        return GetTimeForDay(DateTime.Now.DayOfWeek);
    }
}
