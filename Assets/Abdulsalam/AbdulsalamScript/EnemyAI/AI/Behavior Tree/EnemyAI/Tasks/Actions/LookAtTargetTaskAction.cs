using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LookAtTargetTask", story: "[self] Shlould [Look] at [Target]", category: "Action", id: "6a7b3154cf76622af54f05e50cf87c9e")]
public partial class LookAtTargetTaskAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<EnemyActions> Look;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    public BlackboardVariable<Transform> targetToLookAt;
    private EnemyActions enemyActions;
    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // 1. Check if we can execute the logic
        if (enemyActions == null || targetToLookAt.Value == null)
        {
            // If something is wrong, fail the task.
            return Status.Failure;
        }

        // 2. Pass the target from the Blackboard to our script
        enemyActions.SetTarget(targetToLookAt.Value);

        // 3. Call the logic method on the script
        enemyActions.SmoothLookAtTarget();
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

