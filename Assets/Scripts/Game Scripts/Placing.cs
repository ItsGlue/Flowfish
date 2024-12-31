using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Placing : MonoBehaviour
{
    public Button button; // Reference to the button in the UI
    public Image buttonImage; // Reference to the button's Image component

    // Map each sprite (image) to a corresponding prefab
    public Sprite[] buttonSprites; // Array of button images (states)
    public GameObject[] prefabs;   // Array of prefabs corresponding to each button image

    private GameObject spawnedObject;  // Reference to the spawned object
    public bool isPlacingObject = false; // Flag to check if we are placing an object

    void Update()
    {
        // If we are placing an object, make it follow the mouse
        if (isPlacingObject && spawnedObject != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane; // Set z position based on the camera's near clip plane
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0; // Ensure it's on the same plane for 2D

            spawnedObject.transform.position = worldPosition;

            // Place the object when the left mouse button is clicked
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                isPlacingObject = false; // Stop moving the object

                // After the object is placed, add it to the save list

            }
        }

        Sprite currentSprite = buttonImage.sprite;

        if (buttonImage != null)
        {
            for (int i = 0; i < buttonSprites.Length; i++)
            {
                if (currentSprite == buttonSprites[i])
                {
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null)
                    {
                        if (InventoryManager.instance != null)
                        {
                        int quantity = InventoryManager.instance.GetItemQuantity(prefabs[i].name);
                        buttonText.text = quantity.ToString();
                        }
                    }
                    break; 
                }
            }
        }

    }

    // This method will be called by the button to spawn the object based on the button's current state (image)
    public void SpawnObjectByButtonState()
    {
        // Get the current sprite on the button
        Sprite currentSprite = buttonImage.sprite;

        // Find the corresponding prefab for the current sprite
        for (int i = 0; i < buttonSprites.Length; i++)
        {
            if (currentSprite == buttonSprites[i])
            {
                // Spawn the prefab that matches the current sprite
                if (InventoryManager.instance.GetItemQuantity(prefabs[i].name) > 0) 
                {
                    if (!isPlacingObject)
                    {
                        // Remove item from inventory
                        InventoryManager.instance.RemoveItem(prefabs[i].name);
                        
                        // Instantiate the object
                        spawnedObject = Instantiate(prefabs[i]);
                        
                        // Now we are placing the object
                        isPlacingObject = true;
                        break;
                    }
                }
            }
        }
    }

    // Check if the mouse is over a UI element to avoid placing the object when clicking UI
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
