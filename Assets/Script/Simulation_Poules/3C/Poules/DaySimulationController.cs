using UnityEngine;

public class DaySimulationController : MonoBehaviour
{
    [Header("Simulation Settings")]
    public float secondsPerGameDay = 86400f;  // Durée réelle d'une journée de jeu en secondes
    public float simulationDuration = 5f;    // Durée réelle de la simulation en secondes
    private float simulationSpeed;           // Calculée automatiquement

    private bool isSimulating = false;

    void Start()
    {
        // Calculer la vitesse de simulation pour comprimer une journée en `simulationDuration` secondes
        simulationSpeed = secondsPerGameDay / simulationDuration;
    }

    void Update()
    {
        // Appuyer sur "J" pour commencer la simulation d'une journée
        if (Input.GetKeyDown(KeyCode.J) && !isSimulating)
        {
            StartCoroutine(SimulateDay());
        }
    }

    private System.Collections.IEnumerator SimulateDay()
    {
        isSimulating = true;

        float originalTimeScale = Time.timeScale;  // Sauvegarder le TimeScale original
        Time.timeScale = simulationSpeed;          // Accélérer le temps

        Debug.Log("Simulation d'une journée commencée.");

        yield return new WaitForSecondsRealtime(simulationDuration); // Attendre la durée de la simulation

        Time.timeScale = originalTimeScale;        // Rétablir le TimeScale original
        isSimulating = false;

        Debug.Log("Simulation d'une journée terminée.");
    }
}
