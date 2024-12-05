using UnityEngine;
using TMPro;

public class EntityCounter : MonoBehaviour
{
    public TMP_Text counterText;  // Référence au TextMeshPro où le compteur sera affiché

    private int erikaCount = 0;   // Compteur pour les "Erika" (proies)
    private int predatorCount = 0; // Compteur pour les "Predator" (prédateurs)

    void Start()
    {
        // Initialisation des compteurs
        UpdateCounts();
    }

    void Update()
    {
        // Compter les objets Erika et Predator à chaque frame
        erikaCount = GameObject.FindGameObjectsWithTag("Erika").Length;
        predatorCount = GameObject.FindGameObjectsWithTag("Predator").Length;

        // Mettre à jour le texte affiché dans TextMeshPro
        UpdateCounts();
    }

    void UpdateCounts()
    {
        // Mettre à jour le texte pour afficher les compteurs des entités
        counterText.text = "Erika: " + erikaCount + "\nPredator: " + predatorCount;
    }
}