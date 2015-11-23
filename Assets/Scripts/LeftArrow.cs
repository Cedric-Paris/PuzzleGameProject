using UnityEngine;
using System.Collections;

public class LeftArrow : ChangeDirection
{

	void Start () {
		newDirection = PlayerMovementController.GO_LEFT;	
	}
}

