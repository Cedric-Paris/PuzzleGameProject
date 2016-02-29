using UnityEngine;
using System.Collections;

public class UpArrowUIDraggableElement : UIDraggableElement {
    
    protected override DraggableElement GetElementBase()
    {
        return ((GameObject) Resources.Load("DestructibleUpArrow")).GetComponent<DraggableElement>();
    }

    protected override string GetAssociatedMenuButtonName()
    {
        return "UpArrowIcon";
    }
}
