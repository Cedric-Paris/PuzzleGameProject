using UnityEngine;
using System.Collections;

public class PickingObjectif : Objectif {

	// Use this for initialization
	void Start () {

	}
	
	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		FinalCase.objectifIterator += 1;
		return effect;
	}
}
