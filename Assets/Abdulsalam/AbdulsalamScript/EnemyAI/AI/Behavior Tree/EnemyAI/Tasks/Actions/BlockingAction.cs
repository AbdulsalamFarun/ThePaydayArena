using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Blocking", story: "Enemy [block] the [Target]", category: "Action", id: "8b8c7a3ce39aa172d7ddb7a8ba916d66")]
public partial class BlockingAction : Action
{
    [SerializeReference] public BlackboardVariable<PlayerHealth> Block;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

