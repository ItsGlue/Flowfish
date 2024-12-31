using TMPro;
using UnityEngine;
using UnityEngine.UI; // For Button and Image

public class Timer : MonoBehaviour
{
    public float timeRemaining;         // The initial time in seconds
    public TMP_Text timerText;          // Reference to the TMP text to display the timer
    public Button toggleButton;         // The button to start/pause/resume the timer
    public Button resetButton;          // The reset button (separate button)
    public Sprite playSprite;           // Sprite for the play state (paused)
    public Sprite pauseSprite;          // Sprite for the pause state (running)

    private bool isTimerRunning = false;
    private bool isPaused = false;      // To track if the timer is paused
    private float initialTime;          // Stores the initial time to reset the timer
    private Image toggleButtonImage;    // The image component of the toggle button

    void Start()
    {
        // Store the initial time when the timer is started
        initialTime = timeRemaining;

        // Get the button's image component
        toggleButtonImage = toggleButton.GetComponent<Image>();

        // Set the initial timer display
        UpdateTimerDisplay(timeRemaining);

        // Set the initial button sprite to play (paused)
        toggleButtonImage.sprite = playSprite;

        // Hook up the reset button directly (optional)
        resetButton.onClick.AddListener(ResetTimer);

        timerText.alignment = TextAlignmentOptions.Center;
    }

    void Update()
    {
        if (isTimerRunning && !isPaused)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                // Clamp timeRemaining to 0 to prevent going negative
                if (timeRemaining < 0)
                {
                    timeRemaining = 0;
                    MoneyManager.instance.AddMoney(25);
                }

                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                // Stop the timer and ensure it's clamped at 0
                timeRemaining = 0;
                isTimerRunning = false;
                UpdateTimerDisplay(timeRemaining);
                toggleButtonImage.sprite = playSprite; // Set button to play when timer finishes
                
            }
        }

    }


    // Toggle between starting, pausing, and resuming the timer
    public void ToggleTimer()
    {
        if (!isTimerRunning)
        {
            if (timeRemaining > 0) {
                // Start the timer if it hasn't started yet
                isTimerRunning = true;
                isPaused = false;
                toggleButtonImage.sprite = pauseSprite; // Change to pause icon
            }
        }
        else if (isPaused)
        {
            if (timeRemaining > 0) {
                // Resume the timer if it's paused
                isPaused = false;
                toggleButtonImage.sprite = pauseSprite; // Change to pause icon
            }
        }
        else
        {
            // Pause the timer if it's running
            isPaused = true;
            toggleButtonImage.sprite = playSprite; // Change to play icon
        }
    }

    // Reset the timer to the initial value
    public void ResetTimer()
    {
        timeRemaining = initialTime;
        isPaused = false;
        isTimerRunning = true; // Resume the timer after resetting
        UpdateTimerDisplay(timeRemaining);  // Update the display immediately after reset
        toggleButtonImage.sprite = pauseSprite;   // Set toggle button to pause when reset
    }

    // Update the text display for the timer
    private void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
