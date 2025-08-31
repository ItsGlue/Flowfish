using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Placing : MonoBehaviour
{
    [Header("UI References")]
    public Button button;
    public Image buttonImage;

    [Header("Prefab/Data Settings")]
    public Sprite[] buttonSprites;
    public GameObject[] prefabs;

    private GameObject spawnedObject;
    private bool isPlacingObject = false;
    private int currentPrefabIndex = -1;

    private SaveManager persistenceManager;

    private void Awake()
    {
        persistenceManager = FindFirstObjectByType<SaveManager>();

        if (persistenceManager == null)
        {
            Debug.LogError("No ObjectPersistenceManager found in the scene. Make sure one exists!");
        }
    }

    void Update()
    {
        if (isPlacingObject && spawnedObject != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = 0;

            spawnedObject.transform.position = worldPosition;
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
            {
                isPlacingObject = false;
                if (currentPrefabIndex >= 0 && currentPrefabIndex < prefabs.Length)
                {
                    GameObject prefabToPlace = prefabs[currentPrefabIndex];
                    Vector3 finalPosition = spawnedObject.transform.position;
                    Quaternion finalRotation = spawnedObject.transform.rotation;
                    Vector3 finalScale = spawnedObject.transform.localScale;
                    Destroy(spawnedObject);
                    persistenceManager.PlaceObject(prefabToPlace, finalPosition, finalRotation, finalScale);
                }
                currentPrefabIndex = -1;
            }
        }
        if (buttonImage != null)
        {
            Sprite currentSprite = buttonImage.sprite;
            for (int i = 0; i < buttonSprites.Length; i++)
            {
                if (currentSprite == buttonSprites[i])
                {
                    TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                    if (buttonText != null && InventoryManager.instance != null)
                    {
                        int quantity = InventoryManager.instance.GetItemQuantity(prefabs[i].name);
                        buttonText.text = quantity.ToString();
                    }
                    break;
                }
            }
        }
    }
    public void SpawnObjectByButtonState()
    {
        if (persistenceManager == null)
        {
            Debug.LogError("ObjectPersistenceManager reference is missing!");
            return;
        }
        Sprite currentSprite = buttonImage.sprite;
        for (int i = 0; i < buttonSprites.Length; i++)
        {
            if (currentSprite == buttonSprites[i])
            {
                if (InventoryManager.instance.GetItemQuantity(prefabs[i].name) > 0) 
                {
                    if (!isPlacingObject)
                    {
                        InventoryManager.instance.RemoveItem(prefabs[i].name);
                        currentPrefabIndex = i;
                        spawnedObject = Instantiate(prefabs[i]);
                        MakeObjectTransparent(spawnedObject);
                        
                        isPlacingObject = true;
                        break;
                    }
                }
            }
        }
    }
    private void MakeObjectTransparent(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                Color color = materials[i].color;
                color.a = 0.7f;
                materials[i].color = color;
                materials[i].SetFloat("_Mode", 3);
                materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                materials[i].SetInt("_ZWrite", 0);
                materials[i].DisableKeyword("_ALPHATEST_ON");
                materials[i].EnableKeyword("_ALPHABLEND_ON");
                materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                materials[i].renderQueue = 3000;
            }
            renderer.materials = materials;
        }
    }

    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}