using UnityEngine;

public class Camera : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de déplacement
    public float lookSpeedX = 2f; // Vitesse de la rotation horizontale
    public float lookSpeedY = 2f; // Vitesse de la rotation verticale
    public float upperLookLimit = 80f; // Limite de rotation vers le haut
    public float lowerLookLimit = -80f; // Limite de rotation vers le bas
    public float jumpSpeed = 5f; // Vitesse de montée
    public float fallSpeed = 5f; // Vitesse de descente

    private float rotationX = 0f; // Rotation verticale (haut/bas)
    private float rotationY = 0f; // Rotation horizontale (gauche/droite)

    private Vector3 moveDirection = Vector3.zero; // Direction du mouvement de la caméra
    private bool isPaused = false; // État de la pause
    private Vector3 lastPosition; // Dernière position de la caméra avant la pause
    private Quaternion lastRotation; // Dernière rotation de la caméra avant la pause

    void Start()
    {
        // Verrouiller le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Gérer la pause lorsque Tab est pressé
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            // Si le jeu n'est pas en pause, on gère le mouvement et la rotation
            HandleMouseLook();
            HandleMovement();
        }
    }

    // Fonction de gestion de la pause (gel de la caméra)
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Si le jeu est en pause, on fige la position et la rotation de la caméra
            lastPosition = transform.position; // Enregistrer la position actuelle
            lastRotation = transform.rotation; // Enregistrer la rotation actuelle

            // Désactiver le mouvement de la caméra en la bloquant
            Cursor.lockState = CursorLockMode.Confined; // Relâcher le curseur si besoin
            Cursor.visible = true; // Rendre le curseur visible si en pause

        }
        else
        {
            // Si le jeu reprend, réactiver le verrouillage du curseur
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Réinitialiser la position et la rotation de la caméra
            transform.position = lastPosition;
            transform.rotation = lastRotation;
        }
    }

    // Gestion de la rotation de la souris
    void HandleMouseLook()
    {
        // Récupérer les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotation verticale
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit); // Limiter la rotation verticale

        // Rotation horizontale
        rotationY += mouseX;

        // Appliquer les rotations
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    // Gestion des déplacements
    void HandleMovement()
    {
        // Déplacements horizontaux (avant, arrière, gauche, droite)
        float moveX = 0f;
        float moveZ = 0f;
        float moveY = 0f;

        // Si la touche Z est pressée (Avancer)
        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        // Si la touche S est pressée (Reculer)
        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;
        // Si la touche Q est pressée (Aller à gauche)
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        // Si la touche D est pressée (Aller à droite)
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // Déplacements verticaux (monter, descendre)
        if (Input.GetKey(KeyCode.Space))  // Monter
            moveY = 1f;
        if (Input.GetKey(KeyCode.LeftShift))  // Descendre
            moveY = -1f;

        // Définir la direction de mouvement
        // Utiliser la direction de la caméra pour ajuster les mouvements dans l'espace local
        Vector3 forward = transform.forward;  // Direction avant de la caméra
        Vector3 right = transform.right;      // Direction droite de la caméra

        // Normaliser les directions
        forward.y = 0;  // Ne pas inclure la direction verticale dans le mouvement
        right.y = 0;    // Ne pas inclure la direction verticale dans le mouvement
        forward.Normalize();
        right.Normalize();

        // Calculer le mouvement final (dans l'espace local de la caméra)
        moveDirection = (forward * moveZ + right * moveX + transform.up * moveY).normalized;

        // Déplacer la caméra dans la direction calculée
        transform.Translate(moveDirection * moveSpeed * Time.unscaledDeltaTime, Space.World);
    }
}