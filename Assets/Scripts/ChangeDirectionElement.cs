using UnityEngine;
using System.Collections;

public abstract class ChangeDirectionElement : Element
{
	
	protected DirectionProperties newDirection;
	
	
	public override EffectTransformation Effect(bool isTreated = false)
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.newDirection = newDirection;
		eTransf.newPosition = transform.position;
		return eTransf;
	}
}
