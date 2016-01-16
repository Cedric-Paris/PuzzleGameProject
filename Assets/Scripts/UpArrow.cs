using UnityEngine;
using System.Collections;

public class UpArrow : ChangeDirectionElement
{
	void Start () {
		newDirection = PlayerMovementController.GO_UP;	
	}
}

