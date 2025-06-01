using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("AI Components")]
    public Transform target; // Player reference
    public BehaviorTree behaviorTree;

    [Header("AI Settings")]
    public float attackRange = 2f;

    [Header("Animation")]
    public Animator animator;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Auto-find the player if not assigned
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }
    }

    void Update()
    {
        // Always update movement animation
        if (agent != null && animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        // Behavior Tree tick
        if (behaviorTree != null && target != null)
        {
            behaviorTree.Tick(this);
        }
    }

    public void MoveTo(Vector3 destination)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(destination);
            Debug.Log("Enemy moving to player...");
        }
        else
        {
            Debug.LogWarning("NavMeshAgent missing or not on navmesh!");
        }

        

    }

    public void Attack()
    {
        if (animator != null)
            animator.SetTrigger("Attack");
    }
}
