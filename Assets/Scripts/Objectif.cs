using UnityEngine;
using System.Collections;

public class Objectif : Element {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		return effect;
	}
}
