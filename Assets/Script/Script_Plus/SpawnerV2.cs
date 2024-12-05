using UnityEngine;

public class SpawnerV2 : MonoBehaviour
{
    public GameObject preyPrefab;       // Préfabriqué de la proie (Erika)
    public float spawnInterval = 5f;    // Intervalle entre chaque apparition de proie (en secondes)

    public float minX = -50f;           // Limite min de X pour la position aléatoire
    public float maxX = 50f;            // Limite max de X pour la position aléatoire
    public float minZ = -50f;           // Limite min de Z pour la position aléatoire
    public float maxZ = 50f;            // Limite max de Z pour la position aléatoire
    public float yPosition = 0f;        // Position Y fixe (vous pouvez ajuster si vous voulez un spawn à une hauteur spécifique)

    void Start()
    {
        // Commence à invoquer des proies dès le démarrage
        InvokeRepeating("SpawnPrey", 0f, spawnInterval);
    }

    void SpawnPrey()
    {
        // Générer une position aléatoire dans les limites spécifiées
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Créer la position de spawn
        Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);

        // Créer une nouvelle proie (Erika) à la position aléatoire
        Instantiate(preyPrefab, spawnPosition, Quaternion.identity);

        // Optionnel: Afficher la position du spawn pour le debug
        Debug.Log("Proie spawnée à : " + spawnPosition);
    }
}