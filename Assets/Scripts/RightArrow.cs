using UnityEngine;
using System.Collections;

public class RightArrow : ChangeDirection {	 

	void Start () {
		newDirection = PlayerMovementController.GO_RIGHT;	
	}
}