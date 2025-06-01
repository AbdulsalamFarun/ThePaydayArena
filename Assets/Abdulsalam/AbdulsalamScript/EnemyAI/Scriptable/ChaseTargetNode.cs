using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Actions/Chase Target")]
public class ChaseTargetNode : BTNode
{
    public override State Evaluate(EnemyAI ai)
    {
        if (ai == null || ai.target == null)
            return State.Failure;

        float distance = Vector3.Distance(ai.transform.position, ai.target.position);

        if (distance > ai.attackRange)
        {
            ai.MoveTo(ai.target.position);
            return State.Running;
        }

        return State.Success;
    }
}
