using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    public float moveSpeed = 10f;   
    public float boostMultiplier = 2f;      
    public float lookSpeed = 2f;    
    public float verticalSpeed = 5f;    

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float moveForwardBack = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        float moveLeftRight = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;

        float currentMoveSpeed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? boostMultiplier : 1);

        Vector3 moveDirection = transform.forward * moveForwardBack + transform.right * moveLeftRight;
        transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += Vector3.down * verticalSpeed * Time.deltaTime;
        }

        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f); 
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }
}
