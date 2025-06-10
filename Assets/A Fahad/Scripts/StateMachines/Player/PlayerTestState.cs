using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer = 5f;

    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement.x = stateMachine.PlayerMovement.moveInput.x;
        movement.y = 0f;
        movement.z = stateMachine.PlayerMovement.moveInput.y;

        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        if (stateMachine.PlayerMovement.moveInput == Vector2.zero) { return; }
        
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {

    }
}
