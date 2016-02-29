using UnityEngine;
using System.Collections;

public class DownArrowMenuButton : MenuButton
{

    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("DownArrow"))
            return true;
        return false;
    }

    protected override string GetTextName()
    {
        return "DownArrowCount";
    }

    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UIDownArrow");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}