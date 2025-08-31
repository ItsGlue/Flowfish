using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining;
    public TMP_Text timerText;
    public Button toggleButton;
    public Button resetButton;
    public Sprite playSprite;
    public Sprite pauseSprite;

    private bool isTimerRunning = false;
    private bool isPaused = false;
    private float initialTime;
    private Image toggleButtonImage;

    private float timeElapsed = 0f;

    void Start()
    {
        initialTime = timeRemaining;
        toggleButtonImage = toggleButton.GetComponent<Image>();
        UpdateTimerDisplay(timeRemaining);
        toggleButtonImage.sprite = playSprite;
        resetButton.onClick.AddListener(ResetTimer);
        timerText.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {
        if (isTimerRunning && !isPaused)
        {
            if (timeRemaining > 0)
            {
                float delta = Time.deltaTime;
                timeRemaining -= delta;
                timeElapsed += delta;
                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                }

                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerDisplay(timeRemaining);
                TimeTrackingManager.Instance.AddTimeForToday(timeElapsed);
                Debug.Log($"Timer ended. Elapsed time: {timeElapsed:F2} seconds");
                toggleButtonImage.sprite = playSprite;
                MoneyManager.instance.AddMoney(25);
                timeElapsed = 0f;
            }
        }
    }

    public void ToggleTimer()
    {
        if (!isTimerRunning)
        {
            if (timeRemaining > 0)
            {
                isTimerRunning = true;
                isPaused = false;
                toggleButtonImage.sprite = pauseSprite; 
            }
        }
        else if (isPaused)
        {
            if (timeRemaining > 0)
            {
                isPaused = false;
                toggleButtonImage.sprite = pauseSprite;
            }
        }
        else
        {
            isPaused = true;
            toggleButtonImage.sprite = playSprite;
            TimeTrackingManager.Instance.AddTimeForToday(timeElapsed);
            Debug.Log($"Timer paused. Elapsed time: {timeElapsed:F2} seconds");
        }
    }

    public void ResetTimer()
    {
        TimeTrackingManager.Instance.AddTimeForToday(timeElapsed);
        Debug.Log($"Timer reset. Elapsed time: {timeElapsed:F2} seconds");
        timeRemaining = initialTime;
        timeElapsed = 0f;
        isPaused = false;
        isTimerRunning = true;
        toggleButtonImage.sprite = pauseSprite;
        UpdateTimerDisplay(timeRemaining);
    }

    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
