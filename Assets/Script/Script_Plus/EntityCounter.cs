using UnityEngine;
using TMPro;

public class EntityCounter : MonoBehaviour
{
    public TMP_Text counterText;  // R�f�rence au TextMeshPro o� le compteur sera affich�

    private int erikaCount = 0;   // Compteur pour les "Erika" (proies)
    private int predatorCount = 0; // Compteur pour les "Predator" (pr�dateurs)

    void Start()
    {
        // Initialisation des compteurs
        UpdateCounts();
    }

    void Update()
    {
        // Compter les objets Erika et Predator � chaque frame
        erikaCount = GameObject.FindGameObjectsWithTag("Erika").Length;
        predatorCount = GameObject.FindGameObjectsWithTag("Predator").Length;

        // Mettre � jour le texte affich� dans TextMeshPro
        UpdateCounts();
    }

    void UpdateCounts()
    {
        // Mettre � jour le texte pour afficher les compteurs des entit�s
        counterText.text = "Erika: " + erikaCount + "\nPredator: " + predatorCount;
    }
}