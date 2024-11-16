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
    float probReproduction = 0.5f;

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

    // Variables pour la reproduction
    private float timeSinceLastReproduction = 0f;
    public float reproductionCooldown = 10f; // Temps entre les reproductions en secondes

    void Start()
    {
        _pouleDeplacement = GetComponent<PouleDeplacement>(); // Récupérer le script de déplacement

        // Définir une taille aléatoire pour la poule (Taille et vitesse peuvent être ajustées selon besoin)
        _pouleDeplacement.SetSpeed(Random.Range(1f, 5f));

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
    }

    void GererFaimEtSoif()
    {
        _Faim++;
        _Soif++;

        if (_Faim >= _MaxFaim || _Soif >= _MaxSoif)
        {
            Mourir(); // La poule meurt si elle atteint le maximum de faim ou de soif
        }

        // Si la poule a faim ou soif, elle doit constamment chercher la ressource la plus proche
        if (_Faim >= _MaxFaim / 2 || _Soif >= _MaxSoif / 2)
        {
            MettreAJourCible(); // Met à jour la cible de nourriture ou d'eau la plus proche

            // Si la poule est proche de la cible, elle consomme la ressource
            if (hasTarget && target != null && Vector3.Distance(transform.position, target.position) < 1f)
            {
                Consumable consumable = target.GetComponent<Consumable>();

                if (consumable != null && !consumable.isReserved)
                {
                    consumable.isReserved = true; // Réserver la ressource pour cette poule

                    if (target.CompareTag("Food"))
                    {
                        _Faim = 0; // Réinitialiser la faim après consommation
                        _ResourceManager.ConsumeResource(target.gameObject); // Consommer la nourriture
                    }
                    else if (target.CompareTag("Water"))
                    {
                        _Soif = 0; // Réinitialiser la soif après consommation
                        _ResourceManager.ConsumeResource(target.gameObject); // Consommer l'eau
                    }

                    consumable.isReserved = false; // Libérer la ressource après consommation
                    hasTarget = false; // Réinitialiser l'état de recherche pour permettre une nouvelle recherche
                }
                else
                {
                    // Si la ressource est déjà réservée ou consommée, chercher une autre ressource immédiatement
                    hasTarget = false;
                    MettreAJourCible();
                }
            }
        }
    }

    void MettreAJourCible()
    {
        // Chercher la nourriture ou l'eau la plus proche en fonction des besoins
        if (_Faim >= _MaxFaim / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(foodSources);
            hasTarget = target != null;
            if (hasTarget) _pouleDeplacement.SetTarget(target); // Assigner la nouvelle cible au script de déplacement
        }
        else if (_Soif >= _MaxSoif / 2 && !hasTarget)
        {
            target = TrouverPointLePlusProche(waterSources);
            hasTarget = target != null;
            if (hasTarget) _pouleDeplacement.SetTarget(target); // Assigner la nouvelle cible au script de déplacement
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
        Destroy(gameObject); // Détruire l'objet poule si elle meurt
    }

    void GererReproduction()
    {
        // Mettre à jour le temps écoulé depuis la dernière reproduction
        timeSinceLastReproduction += Time.deltaTime;

        // Vérifier si le cooldown est écoulé
        if (timeSinceLastReproduction >= reproductionCooldown)
        {
            // Réinitialiser le temps écoulé pour la prochaine reproduction
            timeSinceLastReproduction = 0f;

            // Vérifier si la probabilité de reproduction est remplie
            if (Random.Range(0f, 1f) < probReproduction)
            {
                Reproduire();  // Appeler la méthode de reproduction
            }
        }
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

            GameObject nouvellePoule = Instantiate(prefabAReproduire, positionNouveauPoule, Quaternion.identity);

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
        foodSources = GameObject.FindGameObjectsWithTag("Food")
            .Select(go => go.transform)
            .ToArray();

        waterSources = GameObject.FindGameObjectsWithTag("Water")
            .Select(go => go.transform)
            .ToArray();
    }

    GameObject GetRandomPrefab()
    {
        // Sélection aléatoire d'un prefab parmi les cinq disponibles
        GameObject[] prefabs = { _PrefabPaul, _PrefabRobert, _PrefabGabin, _PrefabGreggouze, _PrefabAntonette };
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
