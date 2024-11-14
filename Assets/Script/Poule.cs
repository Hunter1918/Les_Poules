using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poule : MonoBehaviour
{
    //Attribut de la poule
    public int _Age = 0;
    public int _CycleDeVieMax = 0;
    public int _Faim = 0;
    public int _MaxFaim = 10;
    public int _MinFaim = 0;
    public int _Soif = 0;
    public int _MaxSoif = 0;
    public int _MinSoif = 0;
    public float _Speed = 0f; //chaque poule à une vitesse differente
    public float _MaxSpeed = 0f;
    public float _MinSpeed = 0f;
    public int _Taille = 0; //les plus gros sont plus lent et inversement

    public GameObject _PrefabPaul;
    public GameObject _PrefabRobert;
    public GameObject _PrefabGabin;
    public GameObject _PrefabGreggouze;
    public GameObject _PrefabAntonette;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
