using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Références aux deux caméras
    public Camera mainCamera;       // La caméra principale (statique)
    public Camera fpsCamera;       // La caméra FPS (vue à la première personne)

    // Références aux scripts de mouvement FPS
    public float moveSpeed = 5f;   // Vitesse de déplacement dans la vue FPS
    public float lookSpeedX = 2f;  // Vitesse de la rotation horizontale
    public float lookSpeedY = 2f;  // Vitesse de la rotation verticale
    public float upperLookLimit = 80f;  // Limite de rotation vers le haut
    public float lowerLookLimit = -80f; // Limite de rotation vers le bas

    private float rotationX = 0f;  // Rotation verticale (haut/bas)
    private float rotationY = 0f;  // Rotation horizontale (gauche/droite)

    private bool isFPSActive = false;  // Indique si la caméra FPS est active

    void Start()
    {
        // S'assurer que la caméra principale est active au début
        if (mainCamera != null && fpsCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
            fpsCamera.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Changer de caméra lorsque la touche V est pressée
        if (Input.GetKeyDown(KeyCode.V))
        {
            ToggleCameras();
        }

        // Si la caméra FPS est active, gérer les mouvements et la rotation de la tête
        if (isFPSActive)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    // Fonction pour basculer entre les caméras
    void ToggleCameras()
    {
        // Activer ou désactiver la caméra FPS et la caméra principale
        isFPSActive = !isFPSActive;

        if (isFPSActive)
        {
            // Désactive la caméra principale et active la caméra FPS
            mainCamera.gameObject.SetActive(false);
            fpsCamera.gameObject.SetActive(true);
        }
        else
        {
            // Réactive la caméra principale et désactive la caméra FPS
            mainCamera.gameObject.SetActive(true);
            fpsCamera.gameObject.SetActive(false);
        }
    }

    // Fonction pour gérer les déplacements de la caméra FPS
    void HandleMovement()
    {
        // Déplacements horizontaux (avant, arrière, gauche, droite)
        float moveX = 0f;
        float moveZ = 0f;

        // Si la touche W est pressée (Avancer)
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        // Si la touche S est pressée (Reculer)
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        // Si la touche A est pressée (Aller à gauche)
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        // Si la touche D est pressée (Aller à droite)
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Calculer la direction de mouvement (en fonction de la caméra)
        Vector3 moveDirection = (transform.forward * moveZ + transform.right * moveX).normalized;

        // Déplacer la caméra dans la direction calculée
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    // Fonction pour gérer la rotation de la tête (mouvement de souris)
    void HandleMouseLook()
    {
        // Récupérer les mouvements de la souris
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