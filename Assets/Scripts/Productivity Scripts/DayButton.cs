using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayButton : MonoBehaviour
{
    public GameObject calendarScript;
    public GameObject inputSaver;
    public DateTime selectedDate; 
    private TMP_Text buttonText;
    private int month;
    private int year;



    void Start()
    {
        CalendarManager calendarManager = calendarScript.GetComponent<CalendarManager>();
        // saveInputField = inputSaver.GetComponent<SaveInputField>();  
        year = calendarManager.getYear();
        month = calendarManager.getMonth();
    }
    public void clicked(Button button)
    {
        year = CalendarManager.Instance.getYear();
        month = CalendarManager.Instance.getMonth();
        Debug.LogError("Month:" + month);
        // Find the TMP_Text component in the button's children
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        if (buttonText == null)
        {
            Debug.LogError("TMP_Text component not found in the button's children!");
            return;
        }

        // Parse the day from the button text
        if (int.TryParse(buttonText.text, out int day))
        {
            // Validate the day based on the year and month
            int daysInMonth = DateTime.DaysInMonth(year, month);
            if (day >= 1 && day <= daysInMonth)
            {
                selectedDate = new DateTime(year, month, day);
                Debug.Log("" + selectedDate.ToString(""));
                DateManager.Instance.SetSelectedDate(selectedDate);
                SceneManager.LoadScene("Notes");
            }
            else
            {
                Debug.LogError($"Invalid day: {day} for month: {month} and year: {year}. Max days in month: {daysInMonth}");
            }
        }
        else
        {
            Debug.LogError("Button text is not a valid day: " + buttonText.text);
        }
    }


}
