using UnityEngine;
using System.Collections;

public class LeftArrowMenuButton : MenuButton
{

    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("LeftArrow"))
            return true;
        return false;
    }

    protected override string GetTextName()
    {
        return "LeftArrowCount";
    }

    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UILeftArrow");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}
