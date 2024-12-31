using UnityEngine;

public class FoodButton : MonoBehaviour
{
    public GameObject prefabToSpawn; // Assign the prefab in the Inspector
    public Transform spawnLocation;  // Optionally assign a spawn location

   

    void OnMouseOver()  
	{
		if (Input.GetMouseButtonDown(0)) // Check if the left mouse button is pressed
        {
            SpawnPrefab();
        }
	}
    public void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, spawnLocation.position, spawnLocation.rotation);
        }
    }
}
