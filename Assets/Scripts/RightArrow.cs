using UnityEngine;
using System.Collections;

/// <summary>
/// ChangeDirectionElement which makes the player go right.
/// </summary>
public class RightArrow : ChangeDirectionElement
{	 
	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Set the <see cref="newDirection"/> attribute to PlayerMovementController.GO_RIGHT.
	/// </summary>
	void Start () {
		newDirection = PlayerMovementController.GO_RIGHT;	
	}
}