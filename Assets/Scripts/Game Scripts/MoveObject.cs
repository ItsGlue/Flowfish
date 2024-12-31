using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;
    private Placing placingScript;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnMouseDown()
    {
       
        isDragging = true; // Start dragging the object
        
    }

    void OnMouseUp()
    {
        isDragging = false; 
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z;
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = targetPosition;
        }
    }
}
