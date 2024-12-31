using UnityEngine;

public class UpdateSpriteFromSource : MonoBehaviour
{
    public GameObject sourceObject;
    public GameObject targetObject;

    private SpriteRenderer sourceSpriteRenderer;
    private SpriteRenderer targetSpriteRenderer;
    private Sprite previousSprite;   

    void Start()
    {
        sourceSpriteRenderer = sourceObject.GetComponent<SpriteRenderer>();
        targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();

        if (sourceSpriteRenderer == null || targetSpriteRenderer == null)
        {
            Debug.LogError("One of the objects is missing a SpriteRenderer component!");
            return;
        }

        previousSprite = sourceSpriteRenderer.sprite;

        targetSpriteRenderer.sprite = previousSprite;
    }

    void Update()
    {
        if (sourceSpriteRenderer.sprite != previousSprite)
        {
            previousSprite = sourceSpriteRenderer.sprite;  
            targetSpriteRenderer.sprite = previousSprite;  
        }
    }
}
