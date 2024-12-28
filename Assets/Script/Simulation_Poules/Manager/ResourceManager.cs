using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject foodPrefab;
    public GameObject waterPrefab;

    public int numberOfFoodSpawns = 5;
    public int numberOfWaterSpawns = 5;
    private int MaxFood = 50;
    private int itefoodpoules = 0;

    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;

    private List<GameObject> activeResources = new List<GameObject>();

    public float spawnInterval = 20f;

    void Start()
    {
        StartCoroutine(GenerateResources());
    }


    IEnumerator GenerateResources()
    {
        while (MaxFood > itefoodpoules)
        {
            SpawnResources(foodPrefab, numberOfFoodSpawns);
            SpawnResources(waterPrefab, numberOfWaterSpawns);

            yield return new WaitForSeconds(spawnInterval);
        }
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
            itefoodpoules++;

            if (!IsPositionOccupied(randomPosition))
            {
                GameObject resource = Instantiate(prefab, randomPosition, Quaternion.identity);

                if (resource.GetComponent<Consumable>() == null)
                {
                    resource.AddComponent<Consumable>();
                }

                activeResources.Add(resource);
            }
            else
            {
                i--;    
            }
        }
    }

    bool IsPositionOccupied(Vector3 position)
    {
        foreach (var resource in activeResources)
        {
            if (resource != null && Vector3.Distance(resource.transform.position, position) < 1f)
            {
                return true;
            }
        }
        return false;   
    }

    public void ConsumeResource(GameObject resource)
    {
        if (resource != null)
        {
            activeResources.Remove(resource);   
            Destroy(resource);
            itefoodpoules--;
        }
        else
        {
            Debug.LogWarning("La ressource est déjà détruite.");
        }
    }
}
