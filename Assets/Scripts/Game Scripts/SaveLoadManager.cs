using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PrefabData
{
    public string prefabName;
    public Vector3 position;
    public Quaternion rotation;
}

[System.Serializable]
public class SaveData
{
    public List<PrefabData> prefabDataList = new List<PrefabData>();
}

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    public List<GameObject> prefabs;
    public string saveFileName = "saveData.json";
    private string savePath;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void SaveData()
    {
        SaveData data = new SaveData();

        foreach (var prefabInstance in instantiatedPrefabs)
        {
            PrefabData prefabData = new PrefabData
            {
                prefabName = prefabInstance.name.Replace("(Clone)", "").Trim(),
                position = prefabInstance.transform.position,
                rotation = prefabInstance.transform.rotation
            };

            data.prefabDataList.Add(prefabData);
        }

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, jsonData);

        Debug.Log($"Data saved to {savePath}");
    }
    public void LoadData()
    {
        if (!File.Exists(savePath))
        {
            Debug.LogWarning("No save file found.");
            return;
        }
        string jsonData = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
        foreach (var prefab in instantiatedPrefabs)
        {
            Destroy(prefab);
        }
        instantiatedPrefabs.Clear();
        foreach (var prefabData in data.prefabDataList)
        {
            GameObject prefab = prefabs.Find(p => p.name == prefabData.prefabName);

            if (prefab != null)
            {
                GameObject instance = Instantiate(prefab, prefabData.position, prefabData.rotation);
                instantiatedPrefabs.Add(instance);
            }
            else
            {
                Debug.LogWarning($"Prefab {prefabData.prefabName} not found in the list.");
            }
        }
        Debug.Log("Data loaded successfully.");
    }

public void AddPrefab(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject instance = Instantiate(prefab, position, rotation);
        instantiatedPrefabs.Add(instance);
    }

    public void ClearSaveData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        foreach (var prefab in instantiatedPrefabs)
        {
            Destroy(prefab);
        }
        instantiatedPrefabs.Clear();

        Debug.Log("Save data cleared.");
    }
}
