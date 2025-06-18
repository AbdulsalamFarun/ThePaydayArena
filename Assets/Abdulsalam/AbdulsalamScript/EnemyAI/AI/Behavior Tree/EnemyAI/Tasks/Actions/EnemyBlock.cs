using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    public bool IsBlocking { get; private set; }

    // Call this from animation or AI logic
    public void StartBlocking() => IsBlocking = true;
    public void StopBlocking() => IsBlocking = false;
}
