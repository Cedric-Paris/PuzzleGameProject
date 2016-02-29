using UnityEngine;
using System.Collections;

public class UpArrowMenuButton : MenuButton
{
    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("UpArrow"))
            return true;
        return false;
    }

    protected override string GetTextName()
    {
        return "UpArrowCount";
    }

    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UIUpArrow");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}
