using UnityEngine;
using System.Collections;

public class Objectif : Element {

	// Use this for initialization
	void Start () {
	
	}

	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		return effect;
	}
}
