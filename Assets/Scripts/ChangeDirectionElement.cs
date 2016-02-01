using UnityEngine;
using System.Collections;

/// <summary>
/// Change the Player's direction.
/// </summary>
public abstract class ChangeDirectionElement : Element
{
	/// <summary>
	/// The new direction.
	/// </summary>
	protected DirectionProperties newDirection;
	
	/// <summary>
	/// Change the current Player's direction to a new direction.
	/// </summary>
	/// <param name="isTreated">If set to <c>true</c> is treated.</param>
	public override EffectTransformation Effect(bool isTreated = false)
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.newDirection = newDirection;
		eTransf.newPosition = transform.position;
		return eTransf;
	}
}
