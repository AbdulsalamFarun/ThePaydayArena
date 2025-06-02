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
    public float jumpForce = 15f;

    Animator animator;

    Rigidbody rb;

    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        // Debug.Log(moveInput);
        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        float speed = horizontalVelocity.magnitude;

        animator.SetFloat("Speed", speed);
    }

    void FixedUpdate()
    {
        //Player Movement
        rb.linearVelocity = new Vector3(moveInput.x * speed, rb.linearVelocity.y, moveInput.y * speed);


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

