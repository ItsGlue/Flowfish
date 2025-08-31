using UnityEngine;

public class ClickAndDrag : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Make sure your camera is tagged as 'MainCamera'.");
        }
        Debug.Log("ClickAndDrag script initialized.");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log($"Object clicked: {hit.collider.gameObject.name}");
                if (hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    Debug.Log("Dragging started.");
                }
            }
            else
            {
                Debug.Log("Nothing was hit by the raycast.");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (isDragging)
            {
                Debug.Log("Dragging stopped.");
            }
            isDragging = false;
        }
        if (isDragging)
        {
            DragObject();
        }
    }

    private void DragObject()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        targetPosition.z = transform.position.z;
        transform.position = targetPosition;

        Debug.Log($"Object moved to position: {transform.position}");
    }
}
