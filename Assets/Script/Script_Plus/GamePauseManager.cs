using UnityEngine;
using TMPro; // N�cessaire pour TextMeshPro

public class GamePauseManager : MonoBehaviour
{
    public TextMeshProUGUI pauseText; // R�f�rence � l'objet TextMeshPro pour afficher le texte de pause
    public MonoBehaviour[] cameraScripts; // Liste des scripts de la cam�ra � d�sactiver
    public float blinkSpeed = 1f; // Vitesse de clignotement du texte (modifiable dans l'inspecteur)
    private bool isPaused = false; // �tat du jeu (pause ou non)
    private float blinkTimer = 0f; // Timer pour g�rer le clignotement

    void Start()
    {
        // Assurez-vous que le texte "Pause" est d�sactiv� au d�marrage
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(false); // D�sactiver le texte au d�marrage
        }
    }

    void Update()
    {
        // V�rifier si la touche TAB est press�e pour basculer la pause
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        // Si le jeu est en pause, g�rer le clignotement du texte
        if (isPaused && pauseText != null)
        {
            HandleBlinkingText();
        }
    }

    // Fonction pour activer/d�sactiver la pause
    void TogglePause()
    {
        isPaused = !isPaused; // Inverser l'�tat de pause

        // Si le jeu est en pause, on l'arr�te et on affiche le texte
        if (isPaused)
        {
            Time.timeScale = 0f; // Mettre le jeu en pause (temps arr�t�)
            if (pauseText != null)
            {
                pauseText.gameObject.SetActive(true); // Afficher le texte "Pause"
            }

            // D�sactiver les scripts de la cam�ra pour emp�cher le mouvement
            SetCameraMovement(false);
        }
        else
        {
            Time.timeScale = 1f; // Reprendre le jeu (temps normal)
            if (pauseText != null)
            {
                pauseText.gameObject.SetActive(false); // Masquer le texte "Pause"
            }

            // R�activer les scripts de la cam�ra pour permettre le mouvement
            SetCameraMovement(true);
        }
    }

    // Fonction pour activer/d�sactiver les scripts de la cam�ra
    void SetCameraMovement(bool isEnabled)
    {
        foreach (MonoBehaviour script in cameraScripts)
        {
            script.enabled = isEnabled;
        }
    }

    // G�rer le clignotement du texte "Pause"
    void HandleBlinkingText()
    {
        blinkTimer += Time.unscaledDeltaTime * blinkSpeed; // Incr�menter le timer de clignotement

        // Alterner l'affichage du texte toutes les secondes en fonction de la vitesse de clignotement
        if (blinkTimer >= 1f)
        {
            pauseText.gameObject.SetActive(!pauseText.gameObject.activeSelf); // Inverser l'�tat du texte (afficher/masquer)
            blinkTimer = 0f; // R�initialiser le timer
        }
    }
}