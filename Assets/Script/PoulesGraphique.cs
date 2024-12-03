using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoulesGraphique : MonoBehaviour
{
    public Text poulesCountText; // UI Text pour afficher le nombre de poules actuel
    public LineRenderer lineRenderer; // LineRenderer pour afficher le graphique
    public float timeInterval = 1f; // Intervalle de temps pour mesurer le nombre de poules
    public List<GameObject> poulesList = new List<GameObject>(); // Liste des poules dans la scène
    public float maxTime = 30f; // Durée totale du graphique en secondes
    private float timeElapsed = 0f; // Temps écoulé depuis le début
    private List<Vector3> graphPoints = new List<Vector3>(); // Points pour le graphique

    void Start()
    {
        // Initialiser le LineRenderer
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        // Mettre à jour la liste des poules
        UpdatePoulesList();

        // Vérifier si l'intervalle de temps est écoulé
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeInterval)
        {
            // Ajouter le point actuel au graphique
            AddGraphPoint();
            timeElapsed = 0f;
        }

        // Mettre à jour le texte UI avec le nombre de poules actuel
        if (poulesCountText != null)
        {
            poulesCountText.text = "Nombre de poules: " + poulesList.Count;
        }
    }

    // Mettre à jour la liste des poules dans la scène
    void UpdatePoulesList()
    {
        poulesList.Clear();
        Poule[] poulesInScene = FindObjectsOfType<Poule>();
        foreach (Poule poule in poulesInScene)
        {
            poulesList.Add(poule.gameObject);
        }
    }

    // Ajouter un point au graphique en fonction du nombre de poules actuel
    void AddGraphPoint()
    {
        if (graphPoints.Count * timeInterval <= maxTime)
        {
            float x = graphPoints.Count * timeInterval;
            float y = poulesList.Count;
            graphPoints.Add(new Vector3(x, y, 0));

            // Mettre à jour le LineRenderer
            lineRenderer.positionCount = graphPoints.Count;
            lineRenderer.SetPositions(graphPoints.ToArray());
        }
    }
}
