using UnityEngine;

public class SpawnerV2 : MonoBehaviour
{
    public GameObject preyPrefab;       // Pr�fabriqu� de la proie (Erika)
    public float spawnInterval = 5f;    // Intervalle entre chaque apparition de proie (en secondes)

    public float minX = -50f;           // Limite min de X pour la position al�atoire
    public float maxX = 50f;            // Limite max de X pour la position al�atoire
    public float minZ = -50f;           // Limite min de Z pour la position al�atoire
    public float maxZ = 50f;            // Limite max de Z pour la position al�atoire
    public float yPosition = 0f;        // Position Y fixe (vous pouvez ajuster si vous voulez un spawn � une hauteur sp�cifique)

    void Start()
    {
        // Commence � invoquer des proies d�s le d�marrage
        InvokeRepeating("SpawnPrey", 0f, spawnInterval);
    }

    void SpawnPrey()
    {
        // G�n�rer une position al�atoire dans les limites sp�cifi�es
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Cr�er la position de spawn
        Vector3 spawnPosition = new Vector3(randomX, yPosition, randomZ);

        // Cr�er une nouvelle proie (Erika) � la position al�atoire
        Instantiate(preyPrefab, spawnPosition, Quaternion.identity);

        // Optionnel: Afficher la position du spawn pour le debug
        Debug.Log("Proie spawn�e � : " + spawnPosition);
    }
}