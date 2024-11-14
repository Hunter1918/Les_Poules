using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float movertical = Input.GetAxis("Vertical");

        Vector3 mouvement = new Vector3(moveHorizontal, 0.00f, movertical).normalized;
        rb.MovePosition(rb.position + mouvement * speed * Time.deltaTime);
    }
}