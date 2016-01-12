using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine.UI;

public class Menu : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static DraggableElement DraggedObject;
    private Dictionary<DraggableElementType, int> Count = new Dictionary<DraggableElementType, int>(); 

    void Start()
    {
        Count.Add(DraggableElementType.UpArrow, 0);
        Count.Add(DraggableElementType.RightArrow, 0);
        Count.Add(DraggableElementType.DownArrow, 0);
        Count.Add(DraggableElementType.LeftArrow, 0);
        Count.Add(DraggableElementType.Jump, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        if (DraggedObject == null)
        {
            Debug.Log("Nobody in.");
            return;
        }
        Debug.Log("Hello " + DraggedObject.name);
        DraggedObject.AwakeOnMenu();
        HandleDroppedObject();
    }

    private void HandleDroppedObject()
    {
        Debug.Log("Type de l'élément droppé = " + DraggedObject.DraggableElementType);
        UpTheRightCount(DraggedObject.DraggableElementType);

        Debug.Log("OnDrop");
    }

    private void UpTheRightCount(DraggableElementType d)
    {
        this.Count[d] ++;
        // POSSIBILITE D'IMPLEMENTER UN PATRON OBSERVATEUR
        RectTransform[] UIObjects = this.transform.parent.GetComponentsInChildren<RectTransform>();
        DraggableElementHandler draggableElementHandler = null;
        Text countToModify = null;
        foreach (var obj in UIObjects)
        {
            if ((draggableElementHandler = obj.GetComponent<DraggableElementHandler>()) != null && (countToModify = obj.GetComponent<Text>()) != null) // obj is a UIText assigned to one DraggableElementType
            {
                if (draggableElementHandler.DraggableElementType == d) // obj is a UIText assigned to the DraggableElementType we are looking for
                    countToModify.text = string.Format("x {0}", this.Count[d]);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        if (DraggedObject == null)
        {
            Debug.Log("Nobody out.");
            return;
        }
        Debug.Log("Byebye " + DraggedObject.name);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging from " + this.name);
    }
}
