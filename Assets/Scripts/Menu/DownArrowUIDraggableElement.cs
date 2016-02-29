using UnityEngine;
using System.Collections;

public class DownArrowUIDraggableElement : UIDraggableElement
{

    protected override DraggableElement GetElementBase()
    {
        return ((GameObject)Resources.Load("DestrutibleDownArrow")).GetComponent<DraggableElement>();
    }

    protected override string GetAssociatedMenuButtonName()
    {
        return "DownArrowIcon";
    }
}