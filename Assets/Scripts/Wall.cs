using UnityEngine;
using System.Collections;

public class Wall : Element {

	// Use this for initialization
	void Start () {
	
	}

	
	public override EffectTransformation Effect () {
		Debug.Log ("Ceci est un obstacle, vous ne pouvez avancer");
		EffectTransformation effect = new EffectTransformation ();
		effect.isObstacle = true;
		return effect;
	}

}
