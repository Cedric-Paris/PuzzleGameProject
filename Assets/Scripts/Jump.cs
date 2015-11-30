using UnityEngine;
using System.Collections;

public class Jump : Special
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	public override EffectTransformation Effect (bool isTreated = false) {
		Debug.Log ("Ceci est une case Jump, attention à l'aterrissage");
		EffectTransformation effect = new EffectTransformation ();
		effect.isStartingJump = true;
		return effect;
	}
}

