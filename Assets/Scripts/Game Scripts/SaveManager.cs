using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
public class SavedObjectData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    
    public SavedObjectData(string name, Vector3 pos, Quaternion rot, Vector3 scl)
    {
        prefabName = name;
        position = pos;
        rotation = rot;
        scale = scl;
    }
}

[Serializable]
public class SceneSaveData
{
    public string sceneName;
    public List<SavedObjectData> objects = new List<SavedObjectData>();
}

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private Transform objectsContainer;
    [SerializeField] private GameObject[] placablePrefabs;
    
    [Header("Settings")]
    [SerializeField] private string saveFileName = "placed_objects.json";
    
    private Dictionary<string, GameObject> prefabLookup = new Dictionary<string, GameObject>();
    private Dictionary<string, SceneSaveData> allSceneSaveData = new Dictionary<string, SceneSaveData>();
    private string currentSceneName;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (var prefab in placablePrefabs)
        {
            prefabLookup[prefab.name] = prefab;
        }
        LoadAllSaveData();
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }
    
    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        SpawnSavedObjects();
    }
    
    public void PlaceObject(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        GameObject newObject = Instantiate(prefab, position, rotation, objectsContainer);
        newObject.transform.localScale = scale;
        newObject.AddComponent<PlacedObject>().prefabName = prefab.name;
        SaveCurrentSceneObjects();
    }

    public void SaveCurrentSceneObjects()
    {
        SceneSaveData sceneData = new SceneSaveData
        {
            sceneName = currentSceneName,
            objects = new List<SavedObjectData>()
        };

        PlacedObject[] placedObjects = FindObjectsOfType<PlacedObject>();
        
        foreach (PlacedObject obj in placedObjects)
        {
            SavedObjectData objectData = new SavedObjectData(
                obj.prefabName,
                obj.transform.position,
                obj.transform.rotation,
                obj.transform.localScale
            );
            
            sceneData.objects.Add(objectData);
        }
        
        allSceneSaveData[currentSceneName] = sceneData;
        SaveAllData();
    }
    
    private void SpawnSavedObjects()
    {
        PlacedObject[] existingObjects = FindObjectsOfType<PlacedObject>();
        foreach (PlacedObject obj in existingObjects)
        {
            Destroy(obj.gameObject);
        }
        
        if (!allSceneSaveData.TryGetValue(currentSceneName, out SceneSaveData sceneData))
        {
            return;
        }
        
        foreach (SavedObjectData objectData in sceneData.objects)
        {
            if (prefabLookup.TryGetValue(objectData.prefabName, out GameObject prefab))
            {
                GameObject newObject = Instantiate(
                    prefab,
                    objectData.position,
                    objectData.rotation,
                    objectsContainer
                );
                
                newObject.transform.localScale = objectData.scale;
                newObject.AddComponent<PlacedObject>().prefabName = objectData.prefabName;
            }
            else
            {
                Debug.LogWarning($"Prefab '{objectData.prefabName}' not found in placable prefabs list!");
            }
        }
    }
    
    private void SaveAllData()
    {
        try
        {
            string json = JsonUtility.ToJson(new { scenes = allSceneSaveData.Values.ToArray() }, true);
            File.WriteAllText(GetSavePath(), json);
            Debug.Log("Objects saved successfully!");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to save object data: {e.Message}");
        }
    }
    
    private void LoadAllSaveData()
    {
        try
        {
            string savePath = GetSavePath();
            
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                var wrapper = JsonUtility.FromJson<SceneSaveDataWrapper>(json);
                allSceneSaveData.Clear();
                if (wrapper.scenes != null)
                {
                    foreach (var sceneData in wrapper.scenes)
                    {
                        allSceneSaveData[sceneData.sceneName] = sceneData;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load object data: {e.Message}");
        }
    }
    
    private string GetSavePath()
    {
        return Path.Combine(Application.persistentDataPath, saveFileName);
    }
    [Serializable]
    private class SceneSaveDataWrapper
    {
        public SceneSaveData[] scenes;
    }
}
public class PlacedObject : MonoBehaviour
{
    public string prefabName;
}