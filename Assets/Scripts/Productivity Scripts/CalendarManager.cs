using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    public static CalendarManager Instance { get; private set; }  // Singleton instance

    public TMP_Text monthText;
    public TMP_Text yearText;
    public Button nextButton;
    public Button prevButton;
    public GridLayoutGroup gridLayoutGroup;

    private int year;
    private int month;
    private DateTime currentDate;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        currentDate = DateTime.Now;
        UpdateMonthYearText();

        // add listeners to the buttons
        nextButton.onClick.AddListener(OnNextButtonClicked);
        prevButton.onClick.AddListener(OnPrevButtonClicked);
        loadCalendar();
    }

    public int getMonth()
    {
        return month;
    }

    public int getYear()
    {
        return year;
    }

    void OnNextButtonClicked()
    {
        currentDate = currentDate.AddMonths(1);
        UpdateMonthYearText();
        loadCalendar();
    }

    void OnPrevButtonClicked()
    {
        currentDate = currentDate.AddMonths(-1);
        UpdateMonthYearText();
        loadCalendar();
    }

    void UpdateMonthYearText()
    {
        monthText.text = currentDate.ToString("MMMM");
        yearText.text = currentDate.ToString("yyyy");

        year = currentDate.Year;
        month = currentDate.Month;

        int daysInMonth = DateTime.DaysInMonth(year, month);
    }

    void loadCalendar()
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        DayOfWeek startDayOfWeek = firstDayOfMonth.DayOfWeek;
        int day = 1;
        int childIndex = 0;
        int daysInMonth = DateTime.DaysInMonth(year, month);
        foreach (Transform child in gridLayoutGroup.transform)
        {
            TextMeshProUGUI tmpText = child.GetComponentInChildren<TextMeshProUGUI>();
            if (childIndex < (int)startDayOfWeek || day > daysInMonth)
            {
                if (tmpText != null)
                {
                    tmpText.text = "";
                }
                childIndex++;
                continue;
            }

            if (tmpText != null)
            {
                tmpText.text = day.ToString();
                day++;
            }
            else
            {
                Debug.LogError("Child does not have a TextMeshProUGUI component!");
            }

            childIndex++;
        }
    }
}
