using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class Draggable : MonoBehaviour {

	//La methode OnMouseDrag necessite un collider sur l'element a bouger 
	public void OnMouseDrag()
	{
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

	public abstract void OnMouseDrop();

}
