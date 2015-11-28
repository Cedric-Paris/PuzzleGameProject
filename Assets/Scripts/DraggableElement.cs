using UnityEngine;
using System.Collections;

public class DraggableElement : Draggable {

	public override void OnMouseDrop()
	{
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
}
