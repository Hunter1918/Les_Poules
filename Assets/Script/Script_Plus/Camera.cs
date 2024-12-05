using UnityEngine;

public class Camera : MonoBehaviour
{
    public float moveSpeed = 5f; // Vitesse de d�placement
    public float lookSpeedX = 2f; // Vitesse de la rotation horizontale
    public float lookSpeedY = 2f; // Vitesse de la rotation verticale
    public float upperLookLimit = 80f; // Limite de rotation vers le haut
    public float lowerLookLimit = -80f; // Limite de rotation vers le bas
    public float jumpSpeed = 5f; // Vitesse de mont�e
    public float fallSpeed = 5f; // Vitesse de descente

    private float rotationX = 0f; // Rotation verticale (haut/bas)
    private float rotationY = 0f; // Rotation horizontale (gauche/droite)

    private Vector3 moveDirection = Vector3.zero; // Direction du mouvement de la cam�ra
    private bool isPaused = false; // �tat de la pause
    private Vector3 lastPosition; // Derni�re position de la cam�ra avant la pause
    private Quaternion lastRotation; // Derni�re rotation de la cam�ra avant la pause

    void Start()
    {
        // Verrouiller le curseur au centre de l'�cran
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // G�rer la pause lorsque Tab est press�
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        if (!isPaused)
        {
            // Si le jeu n'est pas en pause, on g�re le mouvement et la rotation
            HandleMouseLook();
            HandleMovement();
        }
    }

    // Fonction de gestion de la pause (gel de la cam�ra)
    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Si le jeu est en pause, on fige la position et la rotation de la cam�ra
            lastPosition = transform.position; // Enregistrer la position actuelle
            lastRotation = transform.rotation; // Enregistrer la rotation actuelle

            // D�sactiver le mouvement de la cam�ra en la bloquant
            Cursor.lockState = CursorLockMode.Confined; // Rel�cher le curseur si besoin
            Cursor.visible = true; // Rendre le curseur visible si en pause

        }
        else
        {
            // Si le jeu reprend, r�activer le verrouillage du curseur
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // R�initialiser la position et la rotation de la cam�ra
            transform.position = lastPosition;
            transform.rotation = lastRotation;
        }
    }

    // Gestion de la rotation de la souris
    void HandleMouseLook()
    {
        // R�cup�rer les mouvements de la souris
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

    // Gestion des d�placements
    void HandleMovement()
    {
        // D�placements horizontaux (avant, arri�re, gauche, droite)
        float moveX = 0f;
        float moveZ = 0f;
        float moveY = 0f;

        // Si la touche Z est press�e (Avancer)
        if (Input.GetKey(KeyCode.W))
            moveZ = 1f;
        // Si la touche S est press�e (Reculer)
        if (Input.GetKey(KeyCode.S))
            moveZ = -1f;
        // Si la touche Q est press�e (Aller � gauche)
        if (Input.GetKey(KeyCode.A))
            moveX = -1f;
        // Si la touche D est press�e (Aller � droite)
        if (Input.GetKey(KeyCode.D))
            moveX = 1f;

        // D�placements verticaux (monter, descendre)
        if (Input.GetKey(KeyCode.Space))  // Monter
            moveY = 1f;
        if (Input.GetKey(KeyCode.LeftShift))  // Descendre
            moveY = -1f;

        // D�finir la direction de mouvement
        // Utiliser la direction de la cam�ra pour ajuster les mouvements dans l'espace local
        Vector3 forward = transform.forward;  // Direction avant de la cam�ra
        Vector3 right = transform.right;      // Direction droite de la cam�ra

        // Normaliser les directions
        forward.y = 0;  // Ne pas inclure la direction verticale dans le mouvement
        right.y = 0;    // Ne pas inclure la direction verticale dans le mouvement
        forward.Normalize();
        right.Normalize();

        // Calculer le mouvement final (dans l'espace local de la cam�ra)
        moveDirection = (forward * moveZ + right * moveX + transform.up * moveY).normalized;

        // D�placer la cam�ra dans la direction calcul�e
        transform.Translate(moveDirection * moveSpeed * Time.unscaledDeltaTime, Space.World);
    }
}