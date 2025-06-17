using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private float dodgeDuration = 0.3f;
    private float dodgeSpeed = 10f;

    private float elapsedTime = 0f;

    private Vector3 dodgeDirection;

    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 direction) : base(stateMachine)
    {
        // اذا كان التّجاه صفر، نعتمد على التّجاه الأَمَامِي
        if (direction.sqrMagnitude == 0)
            direction = stateMachine.transform.forward;

        dodgeDirection = direction.normalized;
    }

    public override void Enter()
    {
        elapsedTime = 0f;

        // تحديد الأنيميشن حسب التّجاه
        if (dodgeDirection.z > 0.5f)
        {
            stateMachine.Animator.CrossFade("DodgeForward", 0.1f);
        }
        else if (dodgeDirection.z < -0.5f)
        {
            stateMachine.Animator.CrossFade("DodgeBackward", 0.1f);
        }
        else if (dodgeDirection.x > 0.5f)
        {
            stateMachine.Animator.CrossFade("DodgeRight", 0.1f);
        }
        else if (dodgeDirection.x < -0.5f)
        {
            stateMachine.Animator.CrossFade("DodgeLeft", 0.1f);
        }
    }

    public override void Tick(float deltaTime)
    {
        elapsedTime += deltaTime;

        // التحريك بالدّوج
        stateMachine.Controller.Move(dodgeDirection * dodgeSpeed * deltaTime);

        if (elapsedTime >= dodgeDuration)
        {
            // العودة لحالة FreeLook اذا لم يتم التارقت
            if (stateMachine.Targeter.CurrentTarget == null)
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));

            // اذا كان عندك تارقت عاد على التارقت
            else
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));

        }
    }

    public override void Exit()
    {
        // إيقاف التحرك عند الخروج
        stateMachine.Controller.Move(Vector3.zero);
        
    }
    
}

