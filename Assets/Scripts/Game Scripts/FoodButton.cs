using UnityEngine;

public class FoodButton : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnLocation;

   

    void OnMouseOver()  
	{
		if (Input.GetMouseButtonDown(0))
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
