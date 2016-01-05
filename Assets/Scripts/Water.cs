using UnityEngine;
using System.Collections;

public class Water : Element {

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isWater = true;
		return effect;
	}
}
