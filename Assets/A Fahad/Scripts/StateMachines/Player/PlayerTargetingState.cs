using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine){ }

    public override void Enter()
    {
        stateMachine.PlayerMovement.CancelEvent += OnCancel;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.PlayerMovement.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine , 0));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.PlayerMovement.CancelEvent -= OnCancel;

    }

    private void OnCancel()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
