using UnityEngine;
using System.Collections;

public class UpArrow : ChangeDirection
{
	void Start () {
		newDirection = PlayerMovementController.GO_UP;	
	}
}

