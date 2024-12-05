using UnityEngine;
using TMPro; // Nécessaire pour TextMeshPro

public class GamePauseManager : MonoBehaviour
{
    public TextMeshProUGUI pauseText; // Référence à l'objet TextMeshPro pour afficher le texte de pause
    public MonoBehaviour[] cameraScripts; // Liste des scripts de la caméra à désactiver
    public float blinkSpeed = 1f; // Vitesse de clignotement du texte (modifiable dans l'inspecteur)
    private bool isPaused = false; // État du jeu (pause ou non)
    private float blinkTimer = 0f; // Timer pour gérer le clignotement

    void Start()
    {
        // Assurez-vous que le texte "Pause" est désactivé au démarrage
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(false); // Désactiver le texte au démarrage
        }
    }

    void Update()
    {
        // Vérifier si la touche TAB est pressée pour basculer la pause
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        // Si le jeu est en pause, gérer le clignotement du texte
        if (isPaused && pauseText != null)
        {
            HandleBlinkingText();
        }
    }

    // Fonction pour activer/désactiver la pause
    void TogglePause()
    {
        isPaused = !isPaused; // Inverser l'état de pause

        // Si le jeu est en pause, on l'arrête et on affiche le texte
        if (isPaused)
        {
            Time.timeScale = 0f; // Mettre le jeu en pause (temps arrêté)
            if (pauseText != null)
            {
                pauseText.gameObject.SetActive(true); // Afficher le texte "Pause"
            }

            // Désactiver les scripts de la caméra pour empêcher le mouvement
            SetCameraMovement(false);
        }
        else
        {
            Time.timeScale = 1f; // Reprendre le jeu (temps normal)
            if (pauseText != null)
            {
                pauseText.gameObject.SetActive(false); // Masquer le texte "Pause"
            }

            // Réactiver les scripts de la caméra pour permettre le mouvement
            SetCameraMovement(true);
        }
    }

    // Fonction pour activer/désactiver les scripts de la caméra
    void SetCameraMovement(bool isEnabled)
    {
        foreach (MonoBehaviour script in cameraScripts)
        {
            script.enabled = isEnabled;
        }
    }

    // Gérer le clignotement du texte "Pause"
    void HandleBlinkingText()
    {
        blinkTimer += Time.unscaledDeltaTime * blinkSpeed; // Incrémenter le timer de clignotement

        // Alterner l'affichage du texte toutes les secondes en fonction de la vitesse de clignotement
        if (blinkTimer >= 1f)
        {
            pauseText.gameObject.SetActive(!pauseText.gameObject.activeSelf); // Inverser l'état du texte (afficher/masquer)
            blinkTimer = 0f; // Réinitialiser le timer
        }
    }
}