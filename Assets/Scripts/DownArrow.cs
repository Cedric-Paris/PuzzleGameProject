using UnityEngine;
using System.Collections;

public class DownArrow : ChangeDirection
{
	void Start () {
		newDirection = Player.GO_DOWN;	
	}
}
