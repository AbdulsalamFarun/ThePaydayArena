using System;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");

    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    private EnemyHealthBase lastKnownTargetHealth;
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.PlayerMovement.CancelEvent += OnCancel;
        
        stateMachine.Animator.CrossFade(TargetingBlendTreeHash , .2f);


        
        // ----------------------------------------------------
        // تفعيل كاميرا الاستهداف (TargetingCamera)
        // ----------------------------------------------------
        if (stateMachine.targetCam != null)
        {
            // اجعل أولوية كاميرا الاستهداف أعلى من كاميرا الـ Free Look
            stateMachine.targetCam.Priority = 20; 
        }
        if (stateMachine.FreeLookCam != null)
        {
            // اجعل أولوية كاميرا الـ Free Look منخفضة (اختياري ولكن يضمن عدم التداخل)
            stateMachine.FreeLookCam.Priority = 5; // أي قيمة أقل من 20 وأقل من 10 (الافتراضية)
        }

        
    }

    public override void Tick(float deltaTime)
    {
        HandleHealthBar();
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        if (stateMachine.PlayerMovement.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }
        if (stateMachine.PlayerMovement.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }


        Vector3 movement = CalculateMovment();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();



    }



    public override void Exit()
    {
        stateMachine.PlayerMovement.CancelEvent -= OnCancel;

        if (stateMachine.Targeter.CurrentTarget == null && lastKnownTargetHealth != null)
        {
            lastKnownTargetHealth.HideHealthBar();
        }
        // ----------------------------------------------------
        // إلغاء تفعيل كاميرا الاستهداف وإعادة تفعيل FreeLookCamera
        // ----------------------------------------------------
        if (stateMachine.targetCam != null)
        {
            // ارجع أولوية كاميرا الاستهداف إلى قيمة منخفضة جداً
            stateMachine.targetCam.Priority = 1; 
        }
        if (stateMachine.FreeLookCam != null)
        {
            // أعد أولوية كاميرا الـ Free Look إلى قيمتها الافتراضية/العالية لتصبح هي النشطة
            stateMachine.FreeLookCam.Priority = 15; 
        }


    }

    private void HandleHealthBar()
    {
        // Try to get the health component from the current target
        EnemyHealthBase currentTargetHealth = null;
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            currentTargetHealth = stateMachine.Targeter.CurrentTarget.GetComponent<EnemyHealthBase>();
        }



        // If the target has changed
        if (currentTargetHealth != lastKnownTargetHealth)
        {
            // Hide the old one if it exists
            if (lastKnownTargetHealth != null)
            {
                lastKnownTargetHealth.HideHealthBar();
            }

            // Show the new one if it exists
            if (currentTargetHealth != null)
            {
                currentTargetHealth.ShowHealthBar();
            }

            // Update the last known target
            lastKnownTargetHealth = currentTargetHealth;
        }
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalculateMovment()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.PlayerMovement.moveInput.x;
        movement += stateMachine.transform.forward * stateMachine.PlayerMovement.moveInput.y;


        return movement;
    }
    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.PlayerMovement.moveInput.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0 , 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.PlayerMovement.moveInput.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value , 0.1f, deltaTime);
        }


        if (stateMachine.PlayerMovement.moveInput.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0  , 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.PlayerMovement.moveInput.x > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value , 0.1f, deltaTime);
        }
    }
}
