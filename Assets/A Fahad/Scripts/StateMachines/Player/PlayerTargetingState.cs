using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.PlayerMovement.CancelEvent += OnCancel;
        this.stateMachine.targetCam.Priority = 20;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        FaceTarget();
        Vector3 movement = CalculateMovment();
        Move(movement * stateMachine.TargetingMovementSpeed , deltaTime);

        


        // if (stateMachine.PlayerMovement.IsAttacking)
        // {
        //     stateMachine.SwitchState(new PlayerAttackingState(stateMachine , 0));
        //     return;
        // }
    }

    public override void Exit()
    {
        stateMachine.PlayerMovement.CancelEvent -= OnCancel;

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
}
