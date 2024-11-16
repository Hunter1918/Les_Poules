using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouleDeplacement : MonoBehaviour
{
    public float vitesseMax = 10f;                // Vitesse maximale de la poule
    public float vitesseMin = 5f;                // Vitesse minimale de la poule
    public float distanceMinEntrePoules = 1.5f;  // Distance � garder entre les poules
    public float zoneDeDetection = 3f;           // Distance pour d�tecter les autres poules

    private Vector3 directionCourante;
    private Vector3 cibleAleatoire;

    private float changementDirectionCooldown = 30f;  // Temps entre chaque changement de direction al�atoire
    private float tempsDepuisDernierChangement = 0f;

    public LayerMask layerPoules; // Layer des poules pour d�tecter les autres

    private Transform target; // La cible � atteindre (assign�e par le script Poule.cs)

    void Start()
    {
        SetCibleAleatoire(); // Initialiser la cible al�atoire
    }

    void Update()
    {
        G�rerD�placement(); // Met � jour la direction de la poule
    }

    public void SetTarget(Transform newTarget)
    {
        // Cette fonction permet � Poule.cs de d�finir une cible sp�cifique (nourriture ou eau)
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        // Cette fonction permet � Poule.cs d'ajuster la vitesse en fonction de la taille
        vitesseMax = newSpeed;
    }

    void G�rerD�placement()
    {
        tempsDepuisDernierChangement += Time.deltaTime;

        Vector3 forceDeSeparation = CalculerForceDeSeparation(); // �vite les autres poules

        if (target != null) // Si une cible sp�cifique (nourriture ou eau) est d�finie
        {
            Vector3 directionVersCible = (target.position - transform.position).normalized;
            directionCourante = directionVersCible + forceDeSeparation;
        }
        else
        {
            if (tempsDepuisDernierChangement > changementDirectionCooldown)
            {
                SetCibleAleatoire(); // Choisir une nouvelle cible al�atoire apr�s un d�lai
                tempsDepuisDernierChangement = 0f;
            }

            Vector3 directionVersCible = (cibleAleatoire - transform.position).normalized;
            directionCourante = directionVersCible + forceDeSeparation;
        }

        // Appliquer la vitesse minimale et maximale
        float vitesse = Mathf.Clamp(vitesseMax, vitesseMin, Mathf.Infinity);

        // D�placement vers la cible avec la vitesse
        transform.position += directionCourante * vitesse * Time.deltaTime;

        // Rotation vers la direction de d�placement
        if (directionCourante != Vector3.zero)
        {
            Quaternion rotationVersCible = Quaternion.LookRotation(directionCourante);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationVersCible, Time.deltaTime * 2f);
        }

        // Si pas de cible et trop proche de la cible al�atoire, d�finir une nouvelle cible
        if (target == null && Vector3.Distance(transform.position, cibleAleatoire) < 0.5f)
        {
            SetCibleAleatoire(); // Si proche de la cible al�atoire, choisir une nouvelle cible
        }
    }

    Vector3 CalculerForceDeSeparation()
    {
        Vector3 forceSeparation = Vector3.zero;
        int voisins = 0;

        // D�tecter les autres poules proches
        Collider[] poulesProches = Physics.OverlapSphere(transform.position, zoneDeDetection, layerPoules);

        foreach (Collider poule in poulesProches)
        {
            if (poule.gameObject != gameObject) // Ne pas tenir compte de soi-m�me
            {
                Vector3 directionFuite = transform.position - poule.transform.position;
                float distance = directionFuite.magnitude;

                if (distance < distanceMinEntrePoules)
                {
                    forceSeparation += directionFuite.normalized / distance;
                    voisins++;
                }
            }
        }

        // Moyenne de la force de s�paration selon le nombre de voisins
        if (voisins > 0)
        {
            forceSeparation /= voisins;
        }

        return forceSeparation;
    }

    void SetCibleAleatoire()
    {
        // D�finir une nouvelle cible al�atoire dans une zone autour de la poule
        cibleAleatoire = new Vector3(
            transform.position.x + Random.Range(-5f, 5f),
            transform.position.y,
            transform.position.z + Random.Range(-5f, 5f)
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, zoneDeDetection); // Affiche la zone de d�tection des autres poules
    }
}
