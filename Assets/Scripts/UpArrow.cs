using UnityEngine;
using System.Collections;

public class UpArrow : ChangeDirection
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	public override EffectTransformation Effect()
	{
		EffectTransformation eTransf = new EffectTransformation ();
		eTransf.isChangingSomething = true;
		eTransf.newDirection = Player.GO_UP;
		return eTransf;
	}
}

