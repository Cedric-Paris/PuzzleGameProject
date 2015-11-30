using UnityEngine;
using System.Collections;

public class Objectif : Element {

	// Use this for initialization
	void Start () {
	
	}

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		return effect;
	}
}
