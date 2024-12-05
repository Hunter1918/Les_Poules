using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // R�f�rences aux deux cam�ras
    public Camera mainCamera;       // La cam�ra principale (statique)
    public Camera fpsCamera;       // La cam�ra FPS (vue � la premi�re personne)

    // R�f�rences aux scripts de mouvement FPS
    public float moveSpeed = 5f;   // Vitesse de d�placement dans la vue FPS
    public float lookSpeedX = 2f;  // Vitesse de la rotation horizontale
    public float lookSpeedY = 2f;  // Vitesse de la rotation verticale
    public float upperLookLimit = 80f;  // Limite de rotation vers le haut
    public float lowerLookLimit = -80f; // Limite de rotation vers le bas

    private float rotationX = 0f;  // Rotation verticale (haut/bas)
    private float rotationY = 0f;  // Rotation horizontale (gauche/droite)

    private bool isFPSActive = false;  // Indique si la cam�ra FPS est active

    void Start()
    {
        // S'assurer que la cam�ra principale est active au d�but
        if (mainCamera != null && fpsCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            fpsCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Changer de cam�ra lorsque la touche V est press�e
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCameras();
        }

        // Si la cam�ra FPS est active, g�rer les mouvements et la rotation de la t�te
        if (isFPSActive)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    // Fonction pour basculer entre les cam�ras
    void ToggleCameras()
    {
        // Activer ou d�sactiver la cam�ra FPS et la cam�ra principale
        isFPSActive = !isFPSActive;

        if (isFPSActive)
        {
            // D�sactive la cam�ra principale et active la cam�ra FPS
            mainCamera.gameObject.SetActive(false);
            fpsCamera.gameObject.SetActive(true);
        }
        else
        {
            // R�active la cam�ra principale et d�sactive la cam�ra FPS
            mainCamera.gameObject.SetActive(true);
            fpsCamera.gameObject.SetActive(false);
        }
    }

    // Fonction pour g�rer les d�placements de la cam�ra FPS
    void HandleMovement()
    {
        // D�placements horizontaux (avant, arri�re, gauche, droite)
        float moveX = 0f;
        float moveZ = 0f;

        // Si la touche W est press�e (Avancer)
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        // Si la touche S est press�e (Reculer)
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        // Si la touche A est press�e (Aller � gauche)
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        // Si la touche D est press�e (Aller � droite)
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Calculer la direction de mouvement (en fonction de la cam�ra)
        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;

        // D�placer la cam�ra dans la direction calcul�e
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    // Fonction pour g�rer la rotation de la t�te (mouvement de souris)
    void HandleMouseLook()
    {
        // R�cup�rer les mouvements de la souris
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        // Rotation verticale (haut/bas)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, lowerLookLimit, upperLookLimit); // Limiter la rotation verticale

        // Rotation horizontale (gauche/droite)
        rotationY += mouseX;

        // Appliquer les rotations
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}