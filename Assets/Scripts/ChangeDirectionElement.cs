using UnityEngine;
using System.Collections;

/// <summary>
/// Element which changes the player direction.
/// </summary>
public abstract class ChangeDirectionElement : Element
{
	/// <summary>
	/// The new direction applied by the element.
	/// </summary>
	protected DirectionProperties newDirection;
	
	/// <summary>
	/// Element Effect. An EffectTransformation object type is returned with the modification applied by the effect.
	/// The EffectTransformation object indicates the new direction(<see cref="newDirection"/>) for the Player.
	/// </summary>
	/// <param name="isTreated">true = the element is treated definitively. / false = the element effect may be requested again.</param>
	public override EffectTransformation Effect(bool isTreated = false)
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.newDirection = newDirection;
		eTransf.newPosition = transform.position;
		return eTransf;
	}
}
