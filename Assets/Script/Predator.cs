using UnityEngine;
using System.Collections;


public class Predator : MonoBehaviour
{

    public float speed = 20.0f;
    public float minDist = 1f;
    public Transform target;
    void Start()
    {
        SetTarget();
    }

    void Update()
    {
        SetTarget();

        if (target == null)
            return;

        transform.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > minDist)
            transform.position += transform.forward * speed * Time.deltaTime;

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Poules")) 
        {
            Destroy(collision.gameObject);
        }
    }
    public void SetTarget()
    {
        if (target == null)
        {

            if (GameObject.FindWithTag("Poules") != null)
            {
                target = GameObject.FindWithTag("Poules").GetComponent<Transform>();
            }
        }
    }

}
