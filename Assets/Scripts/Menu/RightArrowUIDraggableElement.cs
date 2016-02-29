using UnityEngine;
using System.Collections;

public class RightArrowUIDraggableElement : UIDraggableElement
{

    protected override DraggableElement GetElementBase()
    {
        return ((GameObject) Resources.Load("DestrutibleRightArrow")).GetComponent<DraggableElement>();
    }

    protected override string GetAssociatedMenuButtonName()
    {
        return "RightArrowIcon";
    }
}