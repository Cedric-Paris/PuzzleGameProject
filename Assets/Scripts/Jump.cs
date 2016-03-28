using UnityEngine;
using System.Collections;

public class Jump : SpecialElement
{
	
	public override EffectTransformation Effect (bool isTreated = false)
	{
		Debug.Log ("Ceci est une case Jump, attention à l'aterrissage");
		EffectTransformation effect = new EffectTransformation ();
		effect.isStartingJump = true;
		return effect;
	}
}

