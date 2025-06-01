using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Composites/Selector")]
public class BTSelector : BTComposite
{
    public override State Evaluate(EnemyAI ai)
    {
        foreach (BTNode node in children)
        {
            State result = node.Evaluate(ai);
            if (result != State.Failure)
                return result;
        }
        return State.Failure;
    }
}
