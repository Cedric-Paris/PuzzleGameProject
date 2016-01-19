using UnityEngine;
using System.Collections;

public class DownArrow : ChangeDirectionElement
{
	void Start () {
		newDirection = PlayerMovementController.GO_DOWN;	
	}
}
