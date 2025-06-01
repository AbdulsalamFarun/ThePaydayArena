using UnityEngine;

[CreateAssetMenu(menuName = "BehaviorTree/Tree")]
public class BehaviorTree : ScriptableObject
{
    public BTNode rootNode;

    public void Tick(EnemyAI ai)
    {
        rootNode.Evaluate(ai);
    }
}
