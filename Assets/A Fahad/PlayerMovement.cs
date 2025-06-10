using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }

    public Transform groundCheck;
    public float groundDistance = 0.2f;

    private bool jumpRequested = false;
    private bool dogeRequested = false;
    public float jumpForce = 15f;
    public float dogeForce = 5f;

    Rigidbody rb;

    public Transform cameraTransform; // اسحب الكاميرا هنا من Inspector


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
        // احسب اتجاه الحركة بناءً على الكاميرا
        // Vector3 camForward = cameraTransform.forward;
        // Vector3 camRight = cameraTransform.right;

        // تجاهل المحور العمودي (Y) لأننا ما نبغى اللاعب يطير
        // camForward.y = 0;
        // camRight.y = 0;
        // camForward.Normalize();
        // camRight.Normalize();

        // الاتجاه النهائي حسب الكاميرا
        // Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;

        // طبّق الحركة
        // rb.linearVelocity = new Vector3(moveDirection.x * currentSpeed, rb.linearVelocity.y, moveDirection.z * currentSpeed);

        // تدوير اللاعب باتجاه الحركة
        // if (moveDirection.sqrMagnitude > 0.01f)
        // {
        //     transform.forward = moveDirection;
        // }

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

