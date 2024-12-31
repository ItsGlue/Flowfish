using UnityEngine;
using UnityEngine.UI;

public class ButtonSlideIn : MonoBehaviour
{
    public Button[] buttons; // Array to hold references to the buttons
    public float slideDuration = 0.5f; // Duration of the slide-in/out animation
    private Vector2[] originalPositions; // Store original positions for sliding in
    private Vector2[] offScreenPositions; // Off-screen positions for sliding out
    private bool isVisible = false; // Track if buttons are currently visible

    void Start()
    {
        // Initialize arrays to store positions
        originalPositions = new Vector2[buttons.Length];
        offScreenPositions = new Vector2[buttons.Length];

        // Store the original anchored positions of the buttons and calculate off-screen positions
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
            originalPositions[i] = rectTransform.anchoredPosition;

            // Calculate an off-screen position below the visible area
            offScreenPositions[i] = new Vector2(originalPositions[i].x, -Screen.height / 2 - rectTransform.rect.height);
            
            // Start with buttons off-screen
            rectTransform.anchoredPosition = offScreenPositions[i];
        }
    }

    public void OnClickToggle()
    {
        if (isVisible)
        {
            // Slide buttons out (hide them)
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                LeanTween.move(rectTransform, offScreenPositions[i], slideDuration);
            }
        }
        else
        {
            // Slide buttons in (show them)
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                LeanTween.move(rectTransform, originalPositions[i], slideDuration);
            }
        }

        // Toggle visibility state
        isVisible = !isVisible;
    }
}
