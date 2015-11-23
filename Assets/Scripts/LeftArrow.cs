using UnityEngine;
using System.Collections;

public class LeftArrow : ChangeDirection
{

	void Start () {
		newDirection = Player.GO_LEFT;	
	}
}

