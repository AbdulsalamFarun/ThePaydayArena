using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Actions/Attack")]
public class AttackNode : BTNode
{
    public override State Evaluate(EnemyAI ai)
    {
        if (Vector3.Distance(ai.transform.position, ai.target.position) <= ai.attackRange)
        {
            ai.Attack();
            return State.Success;
        }
        return State.Failure;
    }
}
