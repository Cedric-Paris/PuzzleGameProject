using UnityEngine;
using System.Collections;

public class LeftArrowUIDraggableElement : UIDraggableElement
{

    protected override DraggableElement GetElementBase()
    {
        return ((GameObject)Resources.Load("DestrutibleLeftArrow")).GetComponent<DraggableElement>();
    }

    protected override string GetAssociatedMenuButtonName()
    {
        return "LeftArrowIcon";
    }
}