using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Actions/Chase Target")]
public class ChaseTargetNode : BTNode
{
    public override State Evaluate(EnemyAI ai)
    {
        if (Vector3.Distance(ai.transform.position, ai.target.position) > ai.attackRange)
        {
            ai.MoveTo(ai.target.position);
            return State.Running;
        }
        return State.Success;
    }
}
