using UnityEngine;
using UnityEngine.UI;

public class ButtonScrolling : MonoBehaviour
{
    public Button[] buttons; // Array to hold the buttons (e.g., 3 buttons per set)
    public Button leftArrowButton; // Reference to the left arrow button ("<")
    public Button rightArrowButton; // Reference to the right arrow button (">")

    public Sprite[] buttonImages; // Array of sprites for the button images
    private int currentSetIndex = 0; // Tracks which set of buttons is being shown
    private int buttonsPerSet; // Number of buttons per set (e.g., 3)
    
    void Start()
    {
        buttonsPerSet = buttons.Length;
        UpdateButtonSet();
        leftArrowButton.interactable = false;
        leftArrowButton.onClick.AddListener(ScrollLeft);
        rightArrowButton.onClick.AddListener(ScrollRight);
        Debug.Log(buttons[0].GetComponent<Image>().sprite);
    }

    private void UpdateButtonSet()
    {
        for (int i = 0; i < buttonsPerSet; i++)
        {
            int imageIndex = currentSetIndex * buttonsPerSet + i;
            if (imageIndex < buttonImages.Length)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].GetComponent<Image>().sprite = buttonImages[imageIndex];
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
        leftArrowButton.interactable = currentSetIndex > 0; 
        rightArrowButton.interactable = (currentSetIndex + 1) * buttonsPerSet < buttonImages.Length;
    }

    private void ScrollLeft()
    {
        if (currentSetIndex > 0)
        {
            currentSetIndex--;
            UpdateButtonSet();
        }
    }

    private void ScrollRight()
    {
        if ((currentSetIndex + 1) * buttonsPerSet < buttonImages.Length)
        {
            currentSetIndex++;
            UpdateButtonSet();
        }
    }
}
