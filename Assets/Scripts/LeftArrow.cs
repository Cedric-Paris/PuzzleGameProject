using UnityEngine;
using System.Collections;

/// <summary>
/// ChangeDirectionElement which makes the player go left.
/// </summary>
public class LeftArrow : ChangeDirectionElement
{
	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Set the <see cref="newDirection"/> attribute to PlayerMovementController.GO_LEFT.
	/// </summary>
	void Start () {
		newDirection = PlayerMovementController.GO_LEFT;	
	}
}

