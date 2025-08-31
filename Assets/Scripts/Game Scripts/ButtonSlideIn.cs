using UnityEngine;
using UnityEngine.UI;

public class ButtonSlideIn : MonoBehaviour
{
    public Button[] buttons;
    public float slideDuration = 0.5f;
    private Vector2[] originalPositions;
    private Vector2[] offScreenPositions;
    private bool isVisible = false;

    void Start()
    {
        originalPositions = new Vector2[buttons.Length];
        offScreenPositions = new Vector2[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
            originalPositions[i] = rectTransform.anchoredPosition;
            offScreenPositions[i] = new Vector2(originalPositions[i].x, -Screen.height / 2 - rectTransform.rect.height);
            rectTransform.anchoredPosition = offScreenPositions[i];
        }
    }

    public void OnClickToggle()
    {
        if (isVisible)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                LeanTween.move(rectTransform, offScreenPositions[i], slideDuration);
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                LeanTween.move(rectTransform, originalPositions[i], slideDuration);
            }
        }
        isVisible = !isVisible;
    }
}
