using UnityEngine;
using System;

public class DateManager : MonoBehaviour
{
    public static DateManager Instance { get; private set; }
    public DateTime SelectedDate { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetSelectedDate(DateTime date)
    {
        SelectedDate = date;
        Debug.Log("date set to: " + date);
    }

    public DateTime getSelectedDate() {
        return SelectedDate;
    }
}
