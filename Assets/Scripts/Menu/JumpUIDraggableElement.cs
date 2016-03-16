using UnityEngine;
using System.Collections;

/// <summary>
/// The JumpUIDraggableElement is a <see cref="UIDraggableElement"/>. Its use is to specify which elements are bound to the <see cref="UIDraggableElement"/> for the Jump movement.
/// </summary>
public class JumpUIDraggableElement : UIDraggableElement
{
    /// <summary>
    /// Informs which Element the <see cref="UIDraggableElement"/> has to spawn.
    /// </summary>
    /// <returns>The Base Element the <see cref="UIDraggableElement"/> has to spawn</returns>
    protected override DraggableElement GetElementBase()
    {
        return ((GameObject)Resources.Load("DestructibleJump")).GetComponent<DraggableElement>();
    }

    /// <summary>
    /// Informs which button is associated with the <see cref="UIDraggableElement"/>.
    /// </summary>
    /// <returns>The name of the button associated with the <see cref="UIDraggableElement"/>.</returns>
    protected override string GetAssociatedMenuButtonName()
    {
        return "JumpIcon";
    }
}
