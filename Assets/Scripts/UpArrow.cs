using UnityEngine;
using System.Collections;

/// <summary>
/// Up arrow.
/// </summary>
public class UpArrow : ChangeDirectionElement
{
	/// <summary>
	/// Apply a newDirection to the Player to the up.
	/// </summary>
	void Start () {
		newDirection = PlayerMovementController.GO_UP;	
	}
}

