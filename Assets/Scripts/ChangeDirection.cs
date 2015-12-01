using UnityEngine;
using System.Collections;

public abstract class ChangeDirection : Special
{

	protected DirectionProperties newDirection;

	
	// Update is called once per frame
	public override EffectTransformation Effect(bool isTreated = false)
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.isChangingSomething = true;
		eTransf.newDirection = newDirection;
		eTransf.newPosition = transform.position;
		return eTransf;
	}
}

