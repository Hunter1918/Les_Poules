using UnityEngine;
using TMPro;

public class TimeControl : MonoBehaviour
{
    // R�f�rence au composant TMP_Text pour afficher le temps
    public TMP_Text timeText;

    // Variable pour stocker le temps �coul� (en secondes)
    private float timeElapsed = 0f;

    // Vitesse initiale du jeu (normal)
    private float timeScale = 1f;

    // Facteur d'acc�l�ration/d�c�l�ration
    public float timeScaleFactor = 2f;

    // Le multiplicateur pour la vitesse du temps (1x, 2x, 4x, etc.)
    private int accelerationFactor = 1;

    void Update()
    {
        // V�rification des entr�es utilisateur pour acc�l�rer ou ralentir le temps
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Acc�l�rer le temps (multiplier par 2)
            ChangeTimeScale(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Ralentir le temps (diviser par 2)
            ChangeTimeScale(false);
        }

        // Incr�mente le temps �coul� en fonction du timeScale
        timeElapsed += Time.deltaTime * timeScale;

        // Met � jour l'affichage du texte avec le temps �coul�
        UpdateTimeText();
    }

    void ChangeTimeScale(bool isAccelerating)
    {
        if (isAccelerating)
        {
            // Acc�l�rer le temps en augmentant le facteur d'acc�l�ration
            accelerationFactor *= 2;
        }
        else
        {
            // Ralentir le temps en r�duisant le facteur d'acc�l�ration
            accelerationFactor = Mathf.Max(accelerationFactor / 2, 1);  // Limite � 1 pour ne pas aller en dessous
        }

        // Met � jour la vitesse du temps (timeScale = 1x, 2x, 4x, etc.)
        timeScale = accelerationFactor;

        // Limiter le timeScale pour �viter des valeurs trop extr�mes
        timeScale = Mathf.Clamp(timeScale, 0.1f, 100f);

        // Met � jour la vitesse du temps dans Unity
        Time.timeScale = timeScale;

        // Afficher la nouvelle vitesse du temps dans la console pour v�rification
        Debug.Log("TimeScale actuel : " + timeScale);
    }

    void UpdateTimeText()
    {
        // V�rifie si le composant TMP_Text est bien assign�
        if (timeText != null)
        {
            // Calcul du temps �coul� en heures, minutes et secondes
            int hours = Mathf.FloorToInt(timeElapsed / 3600);    // 1 heure = 3600 secondes
            int minutes = Mathf.FloorToInt((timeElapsed % 3600) / 60);  // 1 minute = 60 secondes
            int seconds = Mathf.FloorToInt(timeElapsed % 60);           // Secondes restantes

            // Affiche le temps sous la forme "HH:MM:SS"
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            // Si le TMP_Text n'est pas assign�, afficher un message d'avertissement
            Debug.LogWarning("Le composant TMP_Text n'est pas assign� dans l'inspecteur !");
        }
    }
}