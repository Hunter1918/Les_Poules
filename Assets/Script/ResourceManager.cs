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

    public float spawnInterval = 1f; // Intervalle entre chaque génération de ressources

    void Start()
    {
        // Lancer la génération continue de ressources
        StartCoroutine(GenerateResourcesContinuously());
    }

    IEnumerator GenerateResourcesContinuously()
    {
        while (true) // Cette boucle tourne indéfiniment
        {
            // Générer des ressources à chaque intervalle
            SpawnResources(foodPrefab, numberOfFoodSpawns);
            SpawnResources(waterPrefab, numberOfWaterSpawns);

            // Attendre l'intervalle avant de générer à nouveau
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

            // Vérifier si une ressource existe déjà à cet endroit
            if (!IsPositionOccupied(randomPosition))
            {
                GameObject resource = Instantiate(prefab, randomPosition, Quaternion.identity);

                // Ajouter le script "Consumable" à la ressource si nécessaire
                if (resource.GetComponent<Consumable>() == null)
                {
                    resource.AddComponent<Consumable>();
                }

                activeResources.Add(resource);
            }
            else
            {
                // Si la position est déjà occupée, essayer une nouvelle position
                i--;
            }
        }
    }

    bool IsPositionOccupied(Vector3 position)
    {
        // Vérifier si une ressource existe déjà dans un rayon proche de la position donnée
        foreach (var resource in activeResources)
        {
            if (resource != null && Vector3.Distance(resource.transform.position, position) < 1f) // Rayon de 1 unité
            {
                return true; // La position est occupée
            }
        }
        return false;
    }

    public void ConsumeResource(GameObject resource)
    {
        // Vérifie si la ressource est valide avant de la consommer
        if (resource != null)
        {
            activeResources.Remove(resource);  // Retirer la ressource de la liste avant de la détruire
            DestroyImmediate(resource);  // Détruire immédiatement la ressource
        }
        else
        {
            Debug.LogWarning("La ressource est déjà détruite.");
        }
    }
}
