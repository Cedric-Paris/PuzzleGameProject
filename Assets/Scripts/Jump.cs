using UnityEngine;
using System.Collections;

public class Jump : SpecialElement
{
	
	public override EffectTransformation Effect (bool isTreated = false)
	{
		EffectTransformation effect = new EffectTransformation ();
		effect.isStartingJump = true;
		return effect;
	}
}

