using UnityEngine;
using System.Collections;

public class LeftArrow : ChangeDirectionElement
{

	void Start () {
		newDirection = PlayerMovementController.GO_LEFT;	
	}
}

