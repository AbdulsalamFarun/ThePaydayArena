using UnityEngine;

public abstract class BTNode : ScriptableObject
{
    public enum State { Running, Success, Failure }
    public abstract State Evaluate(EnemyAI ai);
}
