using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoulePaul : MonoBehaviour
{
    public int _Age = 0;
    public int _CycleDeVieMax = 100;
    public int _Faim = 0;
    public int _MaxFaim = 10;
    public int _Soif = 0;
    public int _MaxSoif = 10;
    public float probReproduction = 0.2f;

    public Transform[] foodSources;
    public Transform[] waterSources;

    public ResourceManager _ResourceManager;

    private PouleDeplacement _pouleDeplacement;
    private bool hasTarget = false;
    private Transform target;
    private Transform currentTarget;

    private float timeSinceLastReproduction = 0f;
    public float reproductionCooldown = 10f;

    private bool isPredator = false;

    private int totalPoules = 0;
    private int totalPauls = 0;

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

        GererEtatPaul();

        if (isPredator)
        {
            TrackerPoules(Time.deltaTime);
        }
        else
        {
            GererFaimEtSoif(Time.deltaTime);
            GererReproduction(Time.deltaTime);
        }
    }

    public void SimulateUpdate(float deltaTime)
    {
        _Age += (int)(deltaTime);

        if (isPredator)
        {
            TrackerPoules(deltaTime);
        }
        else
        {
            GererFaimEtSoif(deltaTime);
            GererReproduction(deltaTime);
        }

        if (_Age >= _CycleDeVieMax)
        {
            Mourir();
        }
    }

    void GererEtatPaul()
    {
        totalPoules = GameObject.FindGameObjectsWithTag("Poules").Length;
        totalPauls = GameObject.FindGameObjectsWithTag("Paul").Length;
            if (totalPoules >= totalPauls && !isPredator && totalPoules + totalPauls > 50)
            {
                TransformToPredator();
            }
            else if (isPredator && totalPauls >= totalPoules)
            {
                TransformToPaul();
            }
    }

    void TransformToPredator()
    {
        isPredator = true;
        gameObject.name = "Predator_Paul";
        if (_pouleDeplacement != null)
        {
            _pouleDeplacement.enabled = false;
        }
        //Debug.Log("Paul est devenu un prédateur !");
    }

    void TransformToPaul()
    {
        isPredator = false;
        gameObject.name = "paul_ce_giga_beau_gosse";
        if (_pouleDeplacement != null)
        {
            _pouleDeplacement.enabled = true;
        }
        Debug.Log("Paul est redevenu une poule !");
    }

    void TrackerPoules(float deltaTime)
    {
        if (currentTarget == null)
        {
            GameObject[] allPoules = GameObject.FindGameObjectsWithTag("Poules");
            float closestDistance = Mathf.Infinity;

            foreach (GameObject poule in allPoules)
            {
                if (poule.name != "Paul")
                {
                    float distance = Vector3.Distance(transform.position, poule.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        currentTarget = poule.transform;
                    }
                }
            }
        }

        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.position - transform.position).normalized;
            transform.position += direction * 5f * deltaTime; 

            if (Vector3.Distance(transform.position, currentTarget.position) < 2f)
            {
                //Debug.Log("Paul attaque " + currentTarget.name);
                Destroy(currentTarget.gameObject);
                currentTarget = null;
            }
        }
    }

    private void GererFaimEtSoif(float deltaTime)
    {
        _Faim += 1;
        _Soif += 1;

        if (_Faim >= _MaxFaim || _Soif >= _MaxSoif)
        {
            Mourir();
        }

        if (_Faim >= _MaxFaim / 2 || _Soif >= _MaxSoif / 2)
        {
            MettreAJourCible();

            if (hasTarget && target != null && Vector3.Distance(transform.position, target.position) < 1f)
            {
                Consumable consumable = target.GetComponent<Consumable>();

                if (consumable != null && !consumable.isReserved)
                {
                    consumable.isReserved = true;

                    if (target.CompareTag("Food"))
                    {
                        _Faim = 0;
                        _ResourceManager.ConsumeResource(target.gameObject);
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
            if (hasTarget) _pouleDeplacement.SetTarget(target);
        }
        else if (_Soif >= _MaxSoif / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(waterSources);
            hasTarget = target != null;
            if (hasTarget) _pouleDeplacement.SetTarget(target);
        }
    }

    Transform TrouverPointLePlusProche(Transform[] points)
    {
        Transform pointLePlusProche = null;
        float distanceMin = Mathf.Infinity;

        foreach (Transform point in points)
        {
            if (point != null && point.GetComponent<Consumable>() != null && !point.GetComponent<Consumable>().isReserved)
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
        Debug.Log("Paul se reproduit !");
        GameObject prefabAReproduire = Instantiate(gameObject, transform.position + Random.insideUnitSphere, Quaternion.identity);
        prefabAReproduire.name = "paul_ce_giga_beau_gosse";
        prefabAReproduire.tag = "Paul";
        prefabAReproduire.GetComponent<PoulePaul>().ResetStats();
    }

    public void ResetStats()
    {
        _Age = 0;
        _Faim = 0;
        _Soif = 0;
    }

    void MettreAJourSources()
    {
        foodSources = GameObject.FindGameObjectsWithTag("Food")
            .Select(go => go.transform)
            .ToArray();

        waterSources = GameObject.FindGameObjectsWithTag("Water")
            .Select(go => go.transform)
            .ToArray();
    }
}