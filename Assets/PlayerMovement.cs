using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    Rigidbody rb;

    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        Debug.Log(moveInput);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput.x, 0, moveInput.y);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jumped");
            // Add jump logic here, e.g., apply a force to the Rigidbody
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;

    // }
}

