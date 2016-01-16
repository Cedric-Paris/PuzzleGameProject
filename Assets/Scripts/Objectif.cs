using UnityEngine;
using System.Collections;

public class Objectif : Element {


	public override EffectTransformation Effect (bool isTreated = false)
	{
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		return effect;
	}
}
