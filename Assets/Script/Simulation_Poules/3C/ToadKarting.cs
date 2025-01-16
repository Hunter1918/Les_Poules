using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToadKarting : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float boostMultiplier = 2f;
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rb;
    public float RotationSpeed = 720f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float currentMoveSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1);

        moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = (moveDirection) * currentMoveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);

    }
}
