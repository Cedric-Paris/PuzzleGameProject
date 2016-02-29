using UnityEngine;
using System.Collections;

public class RightArrowMenuButton : MenuButton
{

    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("RightArrow"))
            return true;
        return false;
    }

    protected override string GetTextName()
    {
        return "RightArrowCount";
    }

    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UIRightArrow");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}