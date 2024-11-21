using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorDeplacement : MonoBehaviour
{
    public float vitesseMax = 10f;
    public float vitesseMin = 5f;

    private Vector3 directionCourante;
    private Vector3 cibleAleatoire;


    public LayerMask layerPoules;

    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
