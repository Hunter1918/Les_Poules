using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Poule : MonoBehaviour
{
    public int _Age = 0;
    public int _CycleDeVieMax = 100;
    public int _Faim = 0;
    public int _MaxFaim = 10;
    public int _Soif = 0;
    public int _MaxSoif = 10;
    public float probReproduction = 0.2f;
    public int NbrPoules = 0;

    public Transform[] foodSources;
    public Transform[] waterSources;

    public ResourceManager _ResourceManager;

    public GameObject _PrefabPaul;
    public GameObject _PrefabRobert;
    public GameObject _PrefabGabin;
    public GameObject _PrefabGreggouze;
    public GameObject _PrefabAntonette;

    private PouleDeplacement _pouleDeplacement;
    private bool hasTarget = false;
    private Transform target;

    private float timeSinceLastReproduction = 0f;
    public float reproductionCooldown = 10f;

    public float faimDepletionRate = 1f;
    public float soifDepletionRate = 1f;
    public float timeWithoutFood = 0f;

    void Start()
    {
        Time.timeScale = 1f;
        _pouleDeplacement = GetComponent<PouleDeplacement>();
        _pouleDeplacement.SetSpeed(Random.Range(1f, 5f));

        MettreAJourSources();
    }

    void Update()
    {
        if (transform.position.y < -5f)
        {
            transform.position = Vector3.zero;
        }
        _Age++;

        MettreAJourSources();

        if (_Age >= _CycleDeVieMax)
        {
            Mourir();
        }

        MettreAJourPoules();

        GererFaimEtSoif(Time.deltaTime);
        GererReproduction(Time.deltaTime);
    }

    public void SimulateUpdate(float deltaTime)
    {
        _Age += (int)(deltaTime);
        GererFaimEtSoif(deltaTime);
        GererReproduction(deltaTime);
    }
    private void GererFaimEtSoif(float deltaTime)
    {
        _Faim += (int)(deltaTime * faimDepletionRate);
        _Soif += (int)(deltaTime * soifDepletionRate);

        if (_Faim >= _MaxFaim || _Soif >= _MaxSoif)
        {
            Mourir();
            return;
        }

        if (_Faim >= _MaxFaim / 2 || _Soif >= _MaxSoif / 2)
        {
            MettreAJourCible();

            if (
                hasTarget
                && target != null
                && Vector3.Distance(transform.position, target.position) < 1f
            )
            {
                Consumable consumable = target.GetComponent<Consumable>();

                if (consumable != null && !consumable.isReserved)
                {
                    consumable.isReserved = true;

                    if (target.CompareTag("Food"))
                    {
                        _Faim = 0;
                        _ResourceManager.ConsumeResource(target.gameObject);
                        timeWithoutFood = 0f;
                    }
                    else if (target.CompareTag("Water"))
                    {
                        _Soif = 0;
                        _ResourceManager.ConsumeResource(target.gameObject);
                    }

                    consumable.isReserved = false;
                    hasTarget = false;
                }
                else
                {
                    hasTarget = false;
                    MettreAJourCible();
                }
            }
        }
    }

    private void GererReproduction(float deltaTime)
    {
        timeSinceLastReproduction += deltaTime;

        if (timeSinceLastReproduction >= reproductionCooldown)
        {
            timeSinceLastReproduction = 0f;

            if (Random.Range(0f, 1f) < probReproduction)
            {
                Reproduire();
            }
        }
    }

    void MettreAJourCible()
    {
        if (_Faim >= _MaxFaim / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(foodSources);
            hasTarget = target != null;
            if (hasTarget)
                _pouleDeplacement.SetTarget(target);
        }
        else if (_Soif >= _MaxSoif / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(waterSources);
            hasTarget = target != null;
            if (hasTarget)
                _pouleDeplacement.SetTarget(target);
        }
    }

    void MettreAJourPoules()
    {
        NbrPoules =
            GameObject.FindGameObjectsWithTag("Poules").Length
            + GameObject.FindGameObjectsWithTag("Paul").Length;
        //Debug.Log("Nombre de poules : " + NbrPoules);
    }

    Transform TrouverPointLePlusProche(Transform[] points)
    {
        Transform pointLePlusProche = null;
        float distanceMin = Mathf.Infinity;

        foreach (Transform point in points)
        {
            if (
                point != null
                && point.GetComponent<Consumable>() != null
                && !point.GetComponent<Consumable>().isReserved
            )
            {
                float distance = Vector3.Distance(transform.position, point.position);
                if (distance < distanceMin)
                {
                    distanceMin = distance;
                    pointLePlusProche = point;
                }
            }
        }

        return pointLePlusProche;
    }

    void Mourir()
    {
        Destroy(gameObject);
    }

    void Reproduire()
    {
        Debug.Log("La poule se reproduit !");
        GameObject prefabAReproduire = GetRandomPrefab();
        if (prefabAReproduire != null)
        {
            Vector3 positionNouveauPoule = new Vector3(
                transform.position.x + Random.Range(-1f, 1f),
                transform.position.y,
                transform.position.z + Random.Range(-1f, 1f)
            );

            GameObject nouvellePoule = Instantiate(
                prefabAReproduire,
                positionNouveauPoule,
                Quaternion.identity
            );

            Poule pouleScript = nouvellePoule.GetComponent<Poule>();
            if (pouleScript != null)
            {
                pouleScript.ResetStats();
            }
        }
    }

    public void ResetStats()
    {
        _Age = 0;
        _Faim = 0;
        _Soif = 0;
    }

    void MettreAJourSources()
    {
        foodSources = GameObject
            .FindGameObjectsWithTag("Food")
            .Select(go => go.transform)
            .ToArray();

        waterSources = GameObject
            .FindGameObjectsWithTag("Water")
            .Select(go => go.transform)
            .ToArray();
    }

    GameObject GetRandomPrefab()
    {
        GameObject[] prefabs =
        {
            _PrefabPaul,
            _PrefabRobert,
            _PrefabGabin,
            _PrefabGreggouze,
            _PrefabAntonette,
        };
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
