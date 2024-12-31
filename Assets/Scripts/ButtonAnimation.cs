using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimationg : MonoBehaviour
{
    public RectTransform buttonTransform;  // Reference to the Button's RectTransform
    public float bobbingAmplitude = 10f;   // The amount of bobbing
    public float bobbingSpeed = 2f;        // Speed of the bobbing motion
    private Vector3 initialPosition;       // To store the initial position of the button

    void Start()
    {
        // Store the initial position of the button
        initialPosition = buttonTransform.anchoredPosition;
    }

    void Update()
    {
        // Bob the button's position up and down using a sine wave for smooth motion
        float bobbingOffset = Mathf.Sin(Time.time * bobbingSpeed) * bobbingAmplitude;

        // Apply the offset to the button's anchored position
        buttonTransform.anchoredPosition = new Vector3(initialPosition.x, initialPosition.y + bobbingOffset, initialPosition.z);
    }
}
