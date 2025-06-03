using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;

    public Transform groundCheck;
    public float groundDistance = 0.2f;

    private bool jumpRequested = false;
    private bool dogeRequested = false;
    public float jumpForce = 15f;
    public float dogeForce = 5f;

    Rigidbody rb;

    public float speed = 5f;
    public float Sprint = 10f;

    private float currentSpeed;

    private bool isSprinting = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    void FixedUpdate()
    {
        //Player Movement
        rb.linearVelocity = new Vector3(moveInput.x * currentSpeed, rb.linearVelocity.y, moveInput.y * currentSpeed);


        //Player Jump
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
        jumpRequested = false; // Reset jump request after applying force
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {

            jumpRequested = true;
            Debug.Log("Jumped = " + jumpRequested);
        }
    }

    public void OnDoge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // dogeRequested = true;
            Debug.Log("Doged");

            rb.AddRelativeForce(Vector3.forward * dogeForce, ForceMode.Impulse);
        }

    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            // Start sprinting when Shift is held
            currentSpeed = Sprint;
            Debug.Log("Sprinting started");
        }

        if (context.canceled)
        {
            // Stop sprinting when Shift is released
            currentSpeed = speed;
            Debug.Log("Sprinting stopped");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    bool IsGrounded()
    {
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundDistance);
        foreach (Collider hit in hits)
        {
            if (!hit.isTrigger && hit.gameObject != gameObject) // تجاهل نفسه
            {
                return true;
            }
        }
        return false;
    }
}

