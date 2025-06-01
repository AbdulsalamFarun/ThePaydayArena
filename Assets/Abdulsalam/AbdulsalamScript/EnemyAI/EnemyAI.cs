using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float attackRange = 2f;
    public BehaviorTree behaviorTree;

    void Update()
    {
        if (behaviorTree != null)
            behaviorTree.Tick(this);
    }

    // Called by Nodes
    public void MoveTo(Vector3 position)
    {
        GetComponent<NavMeshAgent>().SetDestination(position);

        Debug.Log("Moving to target...");
    }

    public void Attack()
    {
        Debug.Log("Attacking player!");
    }
}
