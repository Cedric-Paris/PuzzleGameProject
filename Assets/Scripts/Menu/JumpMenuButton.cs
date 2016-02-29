using UnityEngine;
using System.Collections;

public class JumpMenuButton : MenuButton
{

    protected override bool IsRightMovementAction(DraggableElement draggableElement)
    {
        if (draggableElement.gameObject.name.Contains("Jump"))
            return true;
        return false;
    }

    protected override string GetTextName()
    {
        return "JumpCount";
    }

    protected override UIDraggableElement GetUIElementToSpawn()
    {
        GameObject UIElementGameObject = (GameObject)Resources.Load("UIElements/UIJump");
        return UIElementGameObject.GetComponent<UIDraggableElement>();
    }
}