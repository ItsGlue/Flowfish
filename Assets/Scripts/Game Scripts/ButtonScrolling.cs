using UnityEngine;
using UnityEngine.UI;

public class ButtonScrolling : MonoBehaviour
{
    public Button[] buttons; 
    public Button leftArrowButton;
    public Button rightArrowButton;

    public Sprite[] buttonImages;
    private int currentSetIndex = 0;
    private int buttonsPerSet;
    
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
