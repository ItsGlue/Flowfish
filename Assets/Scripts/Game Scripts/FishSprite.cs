using UnityEngine;
using UnityEngine.SceneManagement; // Required to access SceneManager

public class UpdateSpriteFromSource : MonoBehaviour
{
    public GameObject sourceObject;
    public GameObject targetObject;

    private SpriteRenderer sourceSpriteRenderer;
    private SpriteRenderer targetSpriteRenderer;
    private Sprite previousSprite;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game")
        {
            
            enabled = false;
            return;
        }

        sourceSpriteRenderer = sourceObject.GetComponent<SpriteRenderer>();
        targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();

        if (sourceSpriteRenderer == null || targetSpriteRenderer == null)
        {
            return;
        }

        previousSprite = sourceSpriteRenderer.sprite;
        targetSpriteRenderer.sprite = previousSprite;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Game") return;

        if (sourceSpriteRenderer.sprite != previousSprite)
        {
            previousSprite = sourceSpriteRenderer.sprite;
            targetSpriteRenderer.sprite = previousSprite;
        }
    }
}
