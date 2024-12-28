using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importation pour TextMeshPro

public class PoulesGraphique : MonoBehaviour
{
    public TMP_Text poulesCountText; // Utilisation de TMP_Text au lieu de Text
    public LineRenderer lineRenderer;
    public float timeInterval = 1f;
    public List<GameObject> poulesList = new List<GameObject>();
    public float maxTime = 30f;
    private float timeElapsed = 0f;
    private List<Vector3> graphPoints = new List<Vector3>();

    void Start()
    {
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        UpdatePoulesList();

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeInterval)
        {
            AddGraphPoint();
            timeElapsed = 0f;
        }

        if (poulesCountText != null)
        {
            poulesCountText.text = "Nombre de poules: " + poulesList.Count;
        }
    }

    void UpdatePoulesList()
    {
        poulesList.Clear();

        // Inclure les poules normales
        Poule[] poulesInScene = FindObjectsOfType<Poule>();
        foreach (Poule poule in poulesInScene)
        {
            poulesList.Add(poule.gameObject);
        }

        // Inclure les Paul
        PoulePaul[] paulsInScene = FindObjectsOfType<PoulePaul>();
        foreach (PoulePaul paul in paulsInScene)
        {
            poulesList.Add(paul.gameObject);
        }
    }

    void AddGraphPoint()
    {
        if (graphPoints.Count * timeInterval <= maxTime)
        {
            float x = graphPoints.Count * timeInterval;
            float y = poulesList.Count;
            graphPoints.Add(new Vector3(x, y, 0));

            lineRenderer.positionCount = graphPoints.Count;
            lineRenderer.SetPositions(graphPoints.ToArray());
        }
    }
}
