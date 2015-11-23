using UnityEngine;
using System.Collections;

public class UpArrow : ChangeDirection
{
	void Start () {
		newDirection = Player.GO_UP;	
	}
}

