using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishChanger : MonoBehaviour
{
    private const string SELECTED_FISH_INDEX_KEY = "SelectedFishIndex";

    public SpriteRenderer targetSpriteRenderer;
    public Sprite[] spriteList;
    public string[] spriteDescriptions;
    public Button nextButton;
    public Button prevButton;
    public TextMeshProUGUI descriptionText;

    private int currentSpriteIndex = 0;

    void Start()
    {
        currentSpriteIndex = PlayerPrefs.GetInt(SELECTED_FISH_INDEX_KEY, 0);
        if (currentSpriteIndex < 0 || currentSpriteIndex >= spriteList.Length)
        {
            currentSpriteIndex = 0;
        }
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
            PlayerPrefs.SetInt(SELECTED_FISH_INDEX_KEY, currentSpriteIndex);
            PlayerPrefs.Save();
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
            PlayerPrefs.SetInt(SELECTED_FISH_INDEX_KEY, currentSpriteIndex);
            PlayerPrefs.Save();
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
        else
        {
            descriptionText.text = string.Empty;
        }
    }
}
