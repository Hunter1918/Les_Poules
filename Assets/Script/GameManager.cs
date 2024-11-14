using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _PrefabPaul;
    public GameObject _PrefabRobert;
    public GameObject _PrefabGabin;
    public GameObject _PrefabGreggouze;
    public GameObject _PrefabAntonette;

    // Cette fonction est appel�e une fois au d�but du jeu
    void Start()
    {
        // Spawner deux poules au d�but du jeu
        SpawnPoule();
        SpawnPoule();
    }

    // Fonction pour spawner une poule al�atoire
    void SpawnPoule()
    {
        GameObject prefabAReproduire = GetRandomPrefab();
        if (prefabAReproduire != null)
        {
            Vector3 positionNouveauPoule = new Vector3(
                Random.Range(-2f, 2f),
                0f,
                Random.Range(-2f, 2f)
            );
            Instantiate(prefabAReproduire, positionNouveauPoule, Quaternion.identity);
        }
    }

    // S�lectionner un prefab al�atoire parmi les poules disponibles
    GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, 5); // Il y a 5 pr�fabriqu�s possibles
        switch (randomIndex)
        {
            case 0: return _PrefabPaul;
            case 1: return _PrefabRobert;
            case 2: return _PrefabGabin;
            case 3: return _PrefabGreggouze;
            case 4: return _PrefabAntonette;
            default: return null;
        }
    }
}
