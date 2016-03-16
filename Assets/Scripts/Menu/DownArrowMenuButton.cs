using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The DownArrowMenuButton is a <see cref="MenuButton"/>. Its use is to specify which elements are bound to the <see cref="MenuButton"/> for the Down Arrow.
/// </summary>
public class DownArrowMenuButton : MenuButton
{
    /// <summary>
    /// Informs whether the given <see cref="Element"/> is the <see cref="DraggableElement"/> bound the this <see cref="MenuButton"/>.
    /// </summary>
    /// <param name="draggableElement">The Element to test.</param>
    /// <returns>true = it is the right element || false = it is not the right element</returns>
    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("DownArrow"))
            return true;
        return false;
    }

    /// <summary>
    /// Informs which <see cref="Text"/> is associated with the <see cref="MenuButton"/>.
    /// </summary>
    /// <returns>The name of the Text associated with the <see cref="MenuButton"/>.</returns>
    protected override string GetTextName()
    {
        return "DownArrowCount";
    }

    /// <summary>
    /// Informs which <see cref="UIDraggableElement"/> the <see cref="MenuButton"/> has to spawn.
    /// </summary>
    /// <returns>The <see cref="UIDraggableElement"/> bound to the <see cref="MenuButton"/>.</returns>
    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UIDownArrow");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}