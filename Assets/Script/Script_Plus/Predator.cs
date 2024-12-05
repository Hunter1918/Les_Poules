/*using UnityEngine;

public class Predator : MonoBehaviour
{
    public string preyTag = "Erika";           // Tag de la proie à chasser
    public float speed = 5f;                   // Vitesse de déplacement du prédateur
    public float detectionRange = 20f;         // Plage de détection du prédateur (distance max à laquelle il peut détecter une proie)
    public float destructionRange = 1f;        // Plage de destruction (distance à laquelle le prédateur détruit la proie)

    private GameObject targetPrey;             // La proie actuellement ciblée par le prédateur
    private int preyEaten = 0;                 // Nombre de proies mangées

    public static int predatorCount = 0;       // Nombre total de prédateurs dans la scène

    [Header("Duplication Settings")]
    [Tooltip("Le nombre maximum de prédateurs autorisé dans la scène.")]
    [SerializeField] private int maxPredators = 10;  // Nombre maximum de prédateurs dans la scène, modifiable dans l'inspecteur

    void Start()
    {
        // Incrémenter le compteur de prédateurs dès que le prédateur est créé
        predatorCount++;

        // Initialiser la proie
        UpdateTargetPrey();
    }

    void Update()
    {
        // Compter les proies et mettre à jour la cible la plus proche
        UpdateTargetPrey();

        // Si une proie est détectée et que le prédateur l'a choisie comme cible
        if (targetPrey != null)
        {
            // Déplacer le prédateur vers la position de la proie
            MoveTowardsPrey();

            // Vérifier si le prédateur entre en collision avec la proie pour la "manger"
            if (Vector3.Distance(transform.position, targetPrey.transform.position) < destructionRange) // Utilisation de destructionRange
            {
                EatPrey();
            }
        }
    }

    void UpdateTargetPrey()
    {
        // Trouver toutes les proies (Erika) dans la scène avec le tag spécifié
        GameObject[] allPrey = GameObject.FindGameObjectsWithTag(preyTag);

        GameObject closestPrey = null;
        float closestDistance = detectionRange;

        // Vérifier la distance entre le prédateur et chaque proie
        foreach (GameObject prey in allPrey)
        {
            float distanceToPrey = Vector3.Distance(transform.position, prey.transform.position);

            // Si la proie est plus proche que la précédente et dans la portée de détection
            if (distanceToPrey < closestDistance)
            {
                closestPrey = prey;
                closestDistance = distanceToPrey;
            }
        }

        // Mettre à jour la cible du prédateur (la proie la plus proche)
        targetPrey = closestPrey;
    }

    void MoveTowardsPrey()
    {
        // Calculer la direction vers la proie
        Vector3 direction = (targetPrey.transform.position - transform.position).normalized;

        // Déplacer le prédateur vers la proie
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void EatPrey()
    {
        // Détruire la proie (effet de disparition)
        Destroy(targetPrey);

        // Incrémenter le compteur de proies mangées
        preyEaten++;

        // Afficher dans la console combien de proies ont été mangées
        Debug.Log("Proie mangée ! Nombre de proies mangées : " + preyEaten);

        // Si le prédateur a mangé 2 proies, il se duplique (si le nombre de prédateurs est inférieur à 10)
        if (preyEaten >= 2 && predatorCount < maxPredators)
        {
            // Créer une nouvelle instance du prédateur
            DuplicatePredator();
            preyEaten = 0; // Réinitialiser le compteur de proies mangées après duplication
        }
    }

    void DuplicatePredator()
    {
        // S'assurer qu'on ne dépasse pas le nombre maximal de prédateurs
        if (predatorCount < maxPredators)
        {
            // Créer une nouvelle instance du prédateur à la même position, mais décalée
            Vector3 spawnPosition = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);  // Décalage sur l'axe X
            Instantiate(gameObject, spawnPosition, transform.rotation);

            // Incrémenter le compteur de prédateurs
            predatorCount++;

            // Afficher dans la console que le prédateur a été dupliqué
            Debug.Log("Prédateur dupliqué ! Nombre total de prédateurs : " + predatorCount);
        }
    }

    // Assurez-vous de décrémenter le compteur lorsque le prédateur est détruit
    void OnDestroy()
    {
        predatorCount--;
    }
}*/