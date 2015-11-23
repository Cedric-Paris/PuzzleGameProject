using UnityEngine;
using System.Collections;

public class Wall : Element {

	// Use this for initialization
	void Start () {
	
	}

	
	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObstacle = true;
		return effect;
	}

}
