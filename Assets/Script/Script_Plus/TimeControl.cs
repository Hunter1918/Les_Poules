using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour
{
    // Référence au composant TMP_Text pour afficher le temps
    public TMP_Text timeText;

    // Variable pour stocker le temps écoulé (en secondes)
    private float timeElapsed = 0f;

    // Vitesse initiale du jeu (normal)
    private float timeScale = 1f;

    // Facteur d'accélération/décélération
    public float timeScaleFactor = 2f;

    // Le multiplicateur pour la vitesse du temps (1x, 2x, 4x, etc.)
    private int accelerationFactor = 1;

    void Update()
    {
        // Vérification des entrées utilisateur pour accélérer ou ralentir le temps
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Accélérer le temps (multiplier par 2)
            ChangeTimeScale(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Ralentir le temps (diviser par 2)
            ChangeTimeScale(false);
        }

        // Incrémente le temps écoulé en fonction du timeScale
        timeElapsed += Time.deltaTime * timeScale;

        // Met à jour l'affichage du texte avec le temps écoulé
        UpdateTimeText();
    }

    void ChangeTimeScale(bool isAccelerating)
    {
        if (isAccelerating)
        {
            // Accélérer le temps en augmentant le facteur d'accélération
            accelerationFactor *= 2;
        }
        else
        {
            // Ralentir le temps en réduisant le facteur d'accélération
            accelerationFactor = Mathf.Max(accelerationFactor / 2, 1);  // Limite à 1 pour ne pas aller en dessous
        }

        // Met à jour la vitesse du temps (timeScale = 1x, 2x, 4x, etc.)
        timeScale = accelerationFactor;

        // Limiter le timeScale pour éviter des valeurs trop extrêmes
        timeScale = Mathf.Clamp(timeScale, 0.1f, 100f);

        // Met à jour la vitesse du temps dans Unity
        Time.timeScale = timeScale;

        // Afficher la nouvelle vitesse du temps dans la console pour vérification
        Debug.Log("TimeScale actuel : " + timeScale);
    }

    void UpdateTimeText()
    {
        // Vérifie si le composant TMP_Text est bien assigné
        if (timeText != null)
        {
            // Calcul du temps écoulé en heures, minutes et secondes
            int hours = Mathf.FloorToInt(timeElapsed / 3600);    // 1 heure = 3600 secondes
            int minutes = Mathf.FloorToInt((timeElapsed % 3600) / 60);  // 1 minute = 60 secondes
            int seconds = Mathf.FloorToInt(timeElapsed % 60);           // Secondes restantes

            // Affiche le temps sous la forme "HH:MM:SS"
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            // Si le TMP_Text n'est pas assigné, afficher un message d'avertissement
            Debug.LogWarning("Le composant TMP_Text n'est pas assigné dans l'inspecteur !");
        }
    }
}