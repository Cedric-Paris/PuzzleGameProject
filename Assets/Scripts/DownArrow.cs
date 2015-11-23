using UnityEngine;
using System.Collections;

public class DownArrow : ChangeDirection
{
	void Start () {
		newDirection = PlayerMovementController.GO_DOWN;	
	}
}
