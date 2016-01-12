using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts;

public class DraggableElement : Draggable
{
    public DraggableElementType DraggableElementType;

    void Start()
    {
        /*RectTransform[] UIObjects = this.transform.parent.GetComponentsInChildren<RectTransform>();
        Canvas canvas = null;
        foreach (var obj in UIObjects)
        {
            canvas = obj.GetComponent<Canvas>();
            if (canvas != null) break;
        }
        menu = canvas.GetComponentInChildren<Menu>();*/
    }

    public void OnMouseDown()
    {
        Debug.Log(this.name + " : OnMouseDown");
        Menu.DraggedObject = this;
    }

	public override void OnMouseDrop()
    {
        Debug.Log(this.name + " : OnMouseDrop");
        Menu.DraggedObject = null;

        Vector3 favoritePosition = new Vector3 (0,0,0);
		favoritePosition.x = CalculDemiLePlusProche (this.transform.position.x);
		favoritePosition.y = CalculDemiLePlusProche (this.transform.position.y);

		this.transform.position = favoritePosition;
	}

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}

    public void AwakeOnMenu()
    {
        Destroy(this.gameObject);
    }
}
