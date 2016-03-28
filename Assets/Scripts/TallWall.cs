using UnityEngine;
using System.Collections;

public class TallWall : Wall {

	public override EffectTransformation Effect (bool isTreated = false)
	{
		EffectTransformation effect = base.Effect(isTreated);
		effect.isTall = true;
		return effect;
	}
}
