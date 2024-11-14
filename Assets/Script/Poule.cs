using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Poule : MonoBehaviour
{
    public int _Age = 0;
    public int _CycleDeVieMax = 100;
    public int _Faim = 0;
    public int _MaxFaim = 10;
    public int _Soif = 0;
    public int _MaxSoif = 10;
    public float _Speed = 5f;
    public float _MaxSpeed = 7f;
    public float _MinSpeed = 3f;
    public int _Taille = 1;

    public Transform[] foodSources;
    public Transform[] waterSources;

    public ResourceManager _ResourceManager;

    private float reproductionCooldown = 10f;
    private float timeSinceLastReproduction = 0f;

    private bool hasTarget = false;
    public Transform target;
    private Vector3 randomTarget;

    void Start()
    {
        if (_ResourceManager == null)
        {
            Debug.LogError("ResourceManager n'a pas été assigné !", this);
        }
        _Taille = Random.Range(1, 5);
        _Speed = Mathf.Clamp(_MaxSpeed - _Taille, _MinSpeed, _MaxSpeed);
        SetRandomTarget();
        MettreAJourSources();
    }

    void Update()
    {
        _Age++;

        if (_Age >= _CycleDeVieMax)
        {
            Mourir();
        }

        GererFaimEtSoif();
        GererReproduction();
        DeplacerPoule();
    }

    void GererFaimEtSoif()
    {
        _Faim++;
        _Soif++;

        if (_Faim >= _MaxFaim || _Soif >= _MaxSoif)
        {
            Mourir();
        }

        // Vérifie si la poule a faim ou soif et cherche la nourriture ou l'eau
        if (_Faim >= _MaxFaim / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(foodSources);
            hasTarget = target != null;
        }
        else if (_Soif >= _MaxSoif / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(waterSources);
            hasTarget = target != null;
        }

        // Si une cible est trouvée, la poule s'y déplace
        if (hasTarget && target != null && Vector3.Distance(transform.position, target.position) < 1f)
        {
            Consumable consumable = target.GetComponent<Consumable>();

            if (consumable != null && !consumable.isReserved)
            {
                consumable.isReserved = true; // Réserve la ressource pour cette poule

                if (target.CompareTag("Food"))
                {
                    _Faim = 0;
                    _ResourceManager.ConsumeResource(target.gameObject); // Consommer la nourriture
                }
                else if (target.CompareTag("Water"))
                {
                    _Soif = 0;
                    _ResourceManager.ConsumeResource(target.gameObject); // Consommer l'eau
                }

                consumable.isReserved = false; // Libère la ressource après consommation
                target = null;
                hasTarget = false;
                MettreAJourSources();  // Mettre à jour la liste des sources après consommation
            }
            else
            {
                // Si la ressource est déjà réservée par une autre poule, recherche une autre cible
                target = null;
                hasTarget = false;
                MettreAJourSources();
            }
        }
    }

    void GererReproduction()
    {
        timeSinceLastReproduction += Time.deltaTime;

        if (timeSinceLastReproduction >= reproductionCooldown)
        {
            timeSinceLastReproduction = 0f;

            if (Random.Range(0f, 1f) < 0.2f)
            {
                Reproduire();
            }
        }
    }

    void Reproduire()
    {
        // Logique de reproduction
    }

    void Mourir()
    {
        Destroy(gameObject);
    }

    void DeplacerPoule()
    {
        if (hasTarget && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * _Speed * Time.deltaTime;
        }
        else
        {
            Vector3 direction = (randomTarget - transform.position).normalized;
            transform.position += direction * _Speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, randomTarget) < 1f)
            {
                SetRandomTarget();
            }
        }
    }

    void SetRandomTarget()
    {
        randomTarget = new Vector3(
            transform.position.x + Random.Range(-5f, 5f),
            transform.position.y,
            transform.position.z + Random.Range(-5f, 5f)
        );
    }

    Transform TrouverPointLePlusProche(Transform[] points)
    {
        Transform pointLePlusProche = null;
        float distanceMin = Mathf.Infinity;

        foreach (Transform point in points)
        {
            // Vérification si le point existe toujours avant de continuer
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
