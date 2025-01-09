using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float FastTime = 5f;

    public int days = 0;
    public int hours = 0;
    public int minutes = 0;
    public float seconds = 0f;

    public float secondsPerGameDay = 86400f;

    private float elapsedTime = 0f;

    public TMP_Text timerText;

    private bool isSimulatingDay = false;
    public float simulationDuration = 5f;

    void Update()
    {
        if (!isSimulatingDay)
        {
            elapsedTime += Time.deltaTime;

            float totalSeconds = elapsedTime;

            days = (int)(totalSeconds / secondsPerGameDay);
            totalSeconds -= days * secondsPerGameDay;

            hours = (int)(totalSeconds / 3600);
            totalSeconds -= hours * 3600;

            minutes = (int)(totalSeconds / 60);
            totalSeconds -= minutes * 60;

            seconds = totalSeconds;

            if (timerText != null)
            {
                timerText.text = string.Format(
                    "Day: {0} Time: {1:00}:{2:00}:{3:00}",
                    days,
                    hours,
                    minutes,
                    (int)seconds
                );
            }
        }

        if (Input.GetKeyDown(KeyCode.J) && !isSimulatingDay)
        {
            StartCoroutine(SimulateDay());
        }
    }

    private IEnumerator SimulateDay()
    {
        isSimulatingDay = true;

        float totalSimulatedTime = 0f;
        float increment = secondsPerGameDay / (simulationDuration * 60);

        Debug.Log("Simulation d'une journée commencée.");

        Poule[] poules = FindObjectsOfType<Poule>();

        while (totalSimulatedTime < secondsPerGameDay)
        {
            totalSimulatedTime += increment;
            elapsedTime += increment;

            foreach (Poule poule in poules)
            {
                if (poule != null)
                {
                    poule.SimulateUpdate(increment);
                }
            }


            yield return null;
        }

        isSimulatingDay = false;
        Debug.Log("Simulation d'une journée terminée.");
    }
}
