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

    void Start()
    {
        SpawnPoule();
        SpawnPoule();
    }

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

    GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, 5); 
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
