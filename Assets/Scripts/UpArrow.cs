using UnityEngine;
using System.Collections;

/// <summary>
/// ChangeDirectionElement which makes the player go up.
/// </summary>
public class UpArrow : ChangeDirectionElement
{
	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Set the <see cref="newDirection"/> attribute to PlayerMovementController.GO_UP.
	/// </summary>
	void Start () {
		newDirection = PlayerMovementController.GO_UP;	
	}
}

