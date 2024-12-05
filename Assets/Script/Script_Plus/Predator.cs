/*using UnityEngine;

public class Predator : MonoBehaviour
{
    public string preyTag = "Erika";           // Tag de la proie � chasser
    public float speed = 5f;                   // Vitesse de d�placement du pr�dateur
    public float detectionRange = 20f;         // Plage de d�tection du pr�dateur (distance max � laquelle il peut d�tecter une proie)
    public float destructionRange = 1f;        // Plage de destruction (distance � laquelle le pr�dateur d�truit la proie)

    private GameObject targetPrey;             // La proie actuellement cibl�e par le pr�dateur
    private int preyEaten = 0;                 // Nombre de proies mang�es

    public static int predatorCount = 0;       // Nombre total de pr�dateurs dans la sc�ne

    [Header("Duplication Settings")]
    [Tooltip("Le nombre maximum de pr�dateurs autoris� dans la sc�ne.")]
    [SerializeField] private int maxPredators = 10;  // Nombre maximum de pr�dateurs dans la sc�ne, modifiable dans l'inspecteur

    void Start()
    {
        // Incr�menter le compteur de pr�dateurs d�s que le pr�dateur est cr��
        predatorCount++;

        // Initialiser la proie
        UpdateTargetPrey();
    }

    void Update()
    {
        // Compter les proies et mettre � jour la cible la plus proche
        UpdateTargetPrey();

        // Si une proie est d�tect�e et que le pr�dateur l'a choisie comme cible
        if (targetPrey != null)
        {
            // D�placer le pr�dateur vers la position de la proie
            MoveTowardsPrey();

            // V�rifier si le pr�dateur entre en collision avec la proie pour la "manger"
            if (Vector3.Distance(transform.position, targetPrey.transform.position) < destructionRange) // Utilisation de destructionRange
            {
                EatPrey();
            }
        }
    }

    void UpdateTargetPrey()
    {
        // Trouver toutes les proies (Erika) dans la sc�ne avec le tag sp�cifi�
        GameObject[] allPrey = GameObject.FindGameObjectsWithTag(preyTag);

        GameObject closestPrey = null;
        float closestDistance = detectionRange;

        // V�rifier la distance entre le pr�dateur et chaque proie
        foreach (GameObject prey in allPrey)
        {
            float distanceToPrey = Vector3.Distance(transform.position, prey.transform.position);

            // Si la proie est plus proche que la pr�c�dente et dans la port�e de d�tection
            if (distanceToPrey < closestDistance)
            {
                closestPrey = prey;
                closestDistance = distanceToPrey;
            }
        }

        // Mettre � jour la cible du pr�dateur (la proie la plus proche)
        targetPrey = closestPrey;
    }

    void MoveTowardsPrey()
    {
        // Calculer la direction vers la proie
        Vector3 direction = (targetPrey.transform.position - transform.position).normalized;

        // D�placer le pr�dateur vers la proie
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void EatPrey()
    {
        // D�truire la proie (effet de disparition)
        Destroy(targetPrey);

        // Incr�menter le compteur de proies mang�es
        preyEaten++;

        // Afficher dans la console combien de proies ont �t� mang�es
        Debug.Log("Proie mang�e ! Nombre de proies mang�es : " + preyEaten);

        // Si le pr�dateur a mang� 2 proies, il se duplique (si le nombre de pr�dateurs est inf�rieur � 10)
        if (preyEaten >= 2 && predatorCount < maxPredators)
        {
            // Cr�er une nouvelle instance du pr�dateur
            DuplicatePredator();
            preyEaten = 0; // R�initialiser le compteur de proies mang�es apr�s duplication
        }
    }

    void DuplicatePredator()
    {
        // S'assurer qu'on ne d�passe pas le nombre maximal de pr�dateurs
        if (predatorCount < maxPredators)
        {
            // Cr�er une nouvelle instance du pr�dateur � la m�me position, mais d�cal�e
            Vector3 spawnPosition = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);  // D�calage sur l'axe X
            Instantiate(gameObject, spawnPosition, transform.rotation);

            // Incr�menter le compteur de pr�dateurs
            predatorCount++;

            // Afficher dans la console que le pr�dateur a �t� dupliqu�
            Debug.Log("Pr�dateur dupliqu� ! Nombre total de pr�dateurs : " + predatorCount);
        }
    }

    // Assurez-vous de d�cr�menter le compteur lorsque le pr�dateur est d�truit
    void OnDestroy()
    {
        predatorCount--;
    }
}*/