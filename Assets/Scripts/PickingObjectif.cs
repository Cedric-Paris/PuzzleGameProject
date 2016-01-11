using UnityEngine;
using System.Collections;

public class PickingObjectif : Objectif {

	// Use this for initialization
	void Start () {
		FinalCase.AddObjectif();
	}
	
	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		if (isTreated) 
		{
			FinalCase.PickObjectif();
		}
		return effect;
	}
}
