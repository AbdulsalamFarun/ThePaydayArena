using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStateMachine stateMachine;
    public Vector2 moveInput { get; private set; }

    public bool IsAttacking { get; private set; }

    public bool IsBlocking { get; private set; }

    public bool dogeRequested { get; private set; }

    public event Action TargetEvent;
    public event Action CancelEvent;

    public Transform groundCheck;
    public float groundDistance = 0.2f;

    private bool jumpRequested = false;
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


/// <summary>
/// Update is called every frame, if the MonoBehaviour is enabled.
/// </summary>
void Update()
{
        // animator.SetBool("BLOCK Animation", IsBlocking);
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
        // if (jumpRequested)
        // {
        //     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // }
        // jumpRequested = false; // Reset jump request after applying force
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

    // public void OnDoge(InputAction.CallbackContext context)
    // {
    //         if (context.performed)
    //         {
    //     // تخبر stateMachine بأتجاه الدّوج حسب مدخلات التحرك الحالية
    //     Vector3 direction = new Vector3();

    //     direction += transform.forward * moveInput.y;
    //     direction += transform.right * moveInput.x;

    //     if (direction.sqrMagnitude == 0)
    //         direction = transform.forward;

    //     stateMachine.SwitchState(new PlayerDodgingState(stateMachine, direction));

    //     Debug.Log("Dodge!");
    //         }

    // }

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

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TargetEvent?.Invoke(); 
            Debug.Log("Targeting event triggered");

        }
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            CancelEvent?.Invoke();
            Debug.Log("Cancel event triggered");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

        public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed && !IsAttacking)
        {
            // animator.Play("BLOCK Animation");

            // Debug.Log("Block");
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            // Debug.Log("Cancel Block");
            IsBlocking = false;
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

