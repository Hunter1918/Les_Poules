using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : MonoBehaviour
{
    public float detectionRadius = 15f;  
    public float speed = 5f;             
    public float timeToEat = 2f;        

    private GameObject targetPoule = null; 
    private bool isEating = false;         
    public int poulesMangees = 0; 

    public int maxPoulesMangees = 10; 

    void Update()
    {
        if (poulesMangees >= maxPoulesMangees)
        {
            Mourir();
            return;
        }

        if (!isEating)
        {
            targetPoule = FindClosestPoule();

            if (targetPoule != null)
            {
                ChasePoule();
            }
        }

        if (targetPoule != null && Vector3.Distance(transform.position, targetPoule.transform.position) < 1.5f)
        {
            StartCoroutine(EatPoule(targetPoule));
        }
    }

    GameObject FindClosestPoule()
    {
        GameObject[] poules = GameObject.FindGameObjectsWithTag("Poule"); 
        GameObject closestPoule = null;
        float closestDistance = detectionRadius;

        foreach (GameObject poule in poules)
        {
            float distanceToPoule = Vector3.Distance(transform.position, poule.transform.position);
            if (distanceToPoule < closestDistance)
            {
                closestPoule = poule;
                closestDistance = distanceToPoule;
            }
        }

        return closestPoule;
    }

    // Poursuivre la poule
    void ChasePoule()
    {
        if (targetPoule != null)
        {
            Vector3 direction = (targetPoule.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            transform.LookAt(targetPoule.transform);
        }
    }

    IEnumerator EatPoule(GameObject poule)
    {
        if (isEating)
            yield break;

        isEating = true;  
        yield return new WaitForSeconds(timeToEat); 

        
        if (poule != null)
        {
            Destroy(poule);
            poulesMangees++;
            Debug.Log("Poules mangées : " + poulesMangees);
        }

        isEating = false;
        targetPoule = null; 
    }

    void Mourir()
    {
        Debug.Log("Le prédateur est mort après avoir mangé " + poulesMangees + " poules.");
        Destroy(gameObject); 
    }
}
