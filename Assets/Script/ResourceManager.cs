using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject foodPrefab; 
    public GameObject waterPrefab;

    public int numberOfFoodSpawns = 5; 
    public int numberOfWaterSpawns = 5; 

    public Vector3 spawnAreaMin; 
    public Vector3 spawnAreaMax; 

    private List<GameObject> activeResources = new List<GameObject>(); 

    void Start()
    {
        SpawnResources(foodPrefab, numberOfFoodSpawns);
        SpawnResources(waterPrefab, numberOfWaterSpawns);
    }

    void SpawnResources(GameObject prefab, int numberOfSpawns)
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                0,
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
            GameObject resource = Instantiate(prefab, randomPosition, Quaternion.identity);

            // Ajouter le script "Consumable" � la ressource
            if (resource.GetComponent<Consumable>() == null)
            {
                resource.AddComponent<Consumable>();
            }

            activeResources.Add(resource);
        }
    }


    public void ConsumeResource(GameObject resource)
    {
        // V�rifie si la ressource est valide avant de la consommer
        if (resource != null)
        {
            activeResources.Remove(resource);
            DestroyImmediate(resource);
            // On peut �ventuellement recr�er la ressource si n�cessaire ici
        }
        else
        {
            Debug.LogWarning("La ressource est d�j� d�truite.");
        }
    }

}
