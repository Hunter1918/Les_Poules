using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private bool isPaused = false;
    private bool isFastForward = false;

    void Update()
    {
        // Pause/Resume quand on appuie sur Échap
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Fast forward (2x) quand on appuie sur T
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFastForward();
        }
    }

    void TogglePause()
    {
        if (isPaused)
        {
            // Reprendre le jeu
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            // Mettre en pause le jeu
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    void ToggleFastForward()
    {
        if (isFastForward)
        {
            // Remettre à la vitesse normale
            Time.timeScale = 1f;
            isFastForward = false;
        }
        else
        {
            // Passer en vitesse rapide (2x)
            Time.timeScale = 2f;
            isFastForward = true;
        }
    }
}
