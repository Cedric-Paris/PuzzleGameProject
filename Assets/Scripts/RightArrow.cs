using UnityEngine;
using System.Collections;

public class RightArrow : ChangeDirectionElement
{	 

	void Start () {
		newDirection = PlayerMovementController.GO_RIGHT;	
	}
}