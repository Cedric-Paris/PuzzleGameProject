using UnityEngine;
using System.Collections;

public class Wall : DeathElement {

	
	public override EffectTransformation Effect (bool isTreated = false)
	{
		EffectTransformation effect = new EffectTransformation ();
		effect.isObstacle = true;
		return effect;
	}

}
