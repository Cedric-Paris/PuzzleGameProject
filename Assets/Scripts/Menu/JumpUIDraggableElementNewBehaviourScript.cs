using UnityEngine;
using System.Collections;

public class JumpUIDraggableElementNewBehaviourScript : UIDraggableElement
{

    protected override DraggableElement GetElementBase()
    {
        return ((GameObject)Resources.Load("DestructibleJump")).GetComponent<DraggableElement>();
    }

    protected override string GetAssociatedMenuButtonName()
    {
        return "JumpIcon";
    }
}
