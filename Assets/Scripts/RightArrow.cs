using UnityEngine;
using System.Collections;

public class RightArrow : ChangeDirection {	 


	// Use this for initialization
	void Start (){

	}


	public override EffectTransformation Effect()
	{
		EffectTransformation eTransf = new EffectTransformation();
		eTransf.isChangingSomething = true;
		eTransf.newDirection=Player.GO_RIGHT;
		return eTransf;
	}
}