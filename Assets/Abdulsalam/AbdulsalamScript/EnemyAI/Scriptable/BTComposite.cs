using System.Collections.Generic;
using UnityEngine;

public abstract class BTComposite : BTNode
{
    [SerializeField] protected List<BTNode> children = new List<BTNode>();

    public void SetChildren(List<BTNode> nodes)
    {
        children = nodes;
    }
}
