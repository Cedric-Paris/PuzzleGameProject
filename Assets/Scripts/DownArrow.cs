using UnityEngine;
using System.Collections;

/// <summary>
/// ChangeDirectionElement which makes the player go down.
/// </summary>
public class DownArrow : ChangeDirectionElement
{
	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Set the <see cref="newDirection"/> attribute to PlayerMovementController.GO_DOWN.
	/// </summary>
	void Start () {
		newDirection = PlayerMovementController.GO_DOWN;	
	}
}
