using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.PlayerMovement.TargetEvent += OnTarget;

        stateMachine.Animator.CrossFade(FreeLookBlendTreeHash,.2f);


        // ----------------------------------------------------
        // تفعيل كاميرا الـ Free Look
        // ----------------------------------------------------
        if (stateMachine.FreeLookCam != null)
        {
            stateMachine.FreeLookCam.Priority = 15; // اجعلها أولوية عالية لتصبح هي النشطة
        }
        if (stateMachine.targetCam != null)
        {
            stateMachine.targetCam.Priority = 1; // اجعلها أولوية منخفضة لتعطيلها
        }

    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.PlayerMovement.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine , 0));
            return;
        }
        Vector3 movement = CalculateMovement();

        Move(movement * stateMachine.FreeLookMovementSpeed , deltaTime);

        if (stateMachine.PlayerMovement.moveInput == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);
        FaceMovementDirection(movement , deltaTime);
    }


    public override void Exit()
    {
        stateMachine.PlayerMovement.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.PlayerMovement.moveInput.y + right * stateMachine.PlayerMovement.moveInput.x;
    }

    private void FaceMovementDirection(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(stateMachine.transform.rotation , Quaternion.LookRotation(movement) , deltaTime * stateMachine.RotationDamping);
    }
}
