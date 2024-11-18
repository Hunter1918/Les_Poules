using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouleDeplacement : MonoBehaviour
{
    public float vitesseMax = 10f;               
    public float vitesseMin = 5f;              
    public float distanceMinEntrePoules = 1.5f;  
    public float zoneDeDetection = 3f;      

    private Vector3 directionCourante;
    private Vector3 cibleAleatoire;

    private float changementDirectionCooldown = 30f;  
    private float tempsDepuisDernierChangement = 0f;

    public LayerMask layerPoules;

    private Transform target;

    void Start()
    {
        SetCibleAleatoire();
    }

    void Update()
    {
        GérerDéplacement(); 
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float newSpeed)
    {
        vitesseMax = newSpeed;
    }

    void GérerDéplacement()
    {
        tempsDepuisDernierChangement += Time.deltaTime;

        Vector3 forceDeSeparation = CalculerForceDeSeparation();

        if (target != null)
        {
            Vector3 directionVersCible = (target.position - transform.position).normalized;
            directionCourante = directionVersCible + forceDeSeparation;
        }
        else
        {
            if (tempsDepuisDernierChangement > changementDirectionCooldown)
            {
                SetCibleAleatoire(); 
                tempsDepuisDernierChangement = 0f;
            }

            Vector3 directionVersCible = (cibleAleatoire - transform.position).normalized;
            directionCourante = directionVersCible + forceDeSeparation;
        }

        float vitesse = Mathf.Clamp(vitesseMax, vitesseMin, Mathf.Infinity);

        transform.position += directionCourante * vitesse * Time.deltaTime;

        if (directionCourante != Vector3.zero)
        {
            Quaternion rotationVersCible = Quaternion.LookRotation(directionCourante);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationVersCible, Time.deltaTime * 2f);
        }

        if (target == null && Vector3.Distance(transform.position, cibleAleatoire) < 0.5f)
        {
            SetCibleAleatoire(); 
        }
    }

    Vector3 CalculerForceDeSeparation()
    {
        Vector3 forceSeparation = Vector3.zero;
        int voisins = 0;

        Collider[] poulesProches = Physics.OverlapSphere(transform.position, zoneDeDetection, layerPoules);

        foreach (Collider poule in poulesProches)
        {
            if (poule.gameObject != gameObject) 
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

        if (voisins > 0)
        {
            forceSeparation /= voisins;
        }

        return forceSeparation;
    }

    void SetCibleAleatoire()
    {
        cibleAleatoire = new Vector3(
            transform.position.x + Random.Range(-5f, 5f),
            transform.position.y,
            transform.position.z + Random.Range(-5f, 5f)
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, zoneDeDetection);
    }
}
