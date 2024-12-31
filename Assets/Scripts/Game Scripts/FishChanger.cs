using UnityEngine;
using UnityEngine.UI;
using TMPro; // Make sure to include this for TMP

public class FishChanger : MonoBehaviour
{
    public SpriteRenderer targetSpriteRenderer;  // GameObject's SpriteRenderer component
    public Sprite[] spriteList;                  // Array of sprites to scroll through
    public string[] spriteDescriptions;           // Array of strings corresponding to each sprite
    public Button nextButton;                    // Button to go to the next sprite
    public Button prevButton;                    // Button to go to the previous sprite
    public TextMeshProUGUI descriptionText;      // TMP object to display the string

    private int currentSpriteIndex = 0;

    void Start()
    {
        if (spriteList.Length > 0)
        {
            targetSpriteRenderer.sprite = spriteList[currentSpriteIndex];
            UpdateDescription();
        }

        nextButton.onClick.AddListener(NextSprite);
        prevButton.onClick.AddListener(PreviousSprite);

        UpdateButtonStates();
    }

    void NextSprite()
    {
        if (currentSpriteIndex < spriteList.Length - 1)
        {
            currentSpriteIndex++;
            targetSpriteRenderer.sprite = spriteList[currentSpriteIndex];
            UpdateDescription();
        }

        UpdateButtonStates();
    }

    void PreviousSprite()
    {
        if (currentSpriteIndex > 0)
        {
            currentSpriteIndex--;
            targetSpriteRenderer.sprite = spriteList[currentSpriteIndex];
            UpdateDescription();
        }

        UpdateButtonStates();
    }

    void UpdateButtonStates()
    {
        prevButton.interactable = currentSpriteIndex > 0;
        nextButton.interactable = currentSpriteIndex < spriteList.Length - 1;
    }

    void UpdateDescription()
    {
        if (currentSpriteIndex < spriteDescriptions.Length)
        {
            descriptionText.text = spriteDescriptions[currentSpriteIndex];
        }
    }
}
