using UnityEngine;
using System.Collections;

public class DraggableElement : Element, Draggable {

    public Element elementBase;

	private Vector3 startPosition;
	private bool isDragging = false;

    //La methode OnMouseDrag necessite un collider sur l'element a bouger 
    public void OnMouseDrag()
    {
		if(!isDragging)
		{
			startPosition = this.transform.position;
			isDragging = true;
		}
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
    }

    public void OnMouseUp()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
        OnMouseDrop();

    }

    public void OnMouseDrop()
	{
		isDragging = false;
        Vector3 favoritePosition = new Vector3(0, 0, 0);
        favoritePosition.x = CalculDemiLePlusProche(this.transform.position.x);
        favoritePosition.y = CalculDemiLePlusProche(this.transform.position.y);
		this.transform.position = startPosition;
		if(this.ElementCanBePlacedHere(favoritePosition))
		{
			this.transform.position = favoritePosition;
		}
		else
		{
			this.transform.position = startPosition;
		}
    }

    public void OnMouseDown()
    {
        Debug.Log(this.name + " : OnMouseDown");
    }

    private float CalculDemiLePlusProche(float value)
    {
        if (value < 0)
            return ((int)value) - 0.5f;
        return ((int)value) + 0.5f;
    }

    public override EffectTransformation Effect(bool isTreated = false)
    {
        return elementBase.Effect(isTreated);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
