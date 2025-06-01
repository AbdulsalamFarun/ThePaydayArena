using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Composites/Sequence")]
public class BTSequence : BTComposite
{
    public override State Evaluate(EnemyAI ai)
    {
        foreach (BTNode node in children)
        {
            State result = node.Evaluate(ai);
            if (result != State.Success)
                return result;
        }
        return State.Success;
    }
}
