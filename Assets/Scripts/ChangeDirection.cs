using UnityEngine;
using System.Collections;

public abstract class ChangeDirection : Special
{

	protected Vector2 newDirection;

	
	// Update is called once per frame
	public override EffectTransformation Effect()
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.isChangingSomething = true;
		eTransf.newDirection = newDirection;
		eTransf.newPosition = transform.position;
		return eTransf;
	}
}

