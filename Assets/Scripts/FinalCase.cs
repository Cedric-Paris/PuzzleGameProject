using UnityEngine;
using System.Collections;

public class FinalCase : Objectif {

	public static int nbobjectif;
	private static Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetBool("isEnabled", true);
	}
	
	// Update is called once per frame
	void Update () {
		if (nbobjectif <= 0) {
			animator.SetBool("isEnabled", true);
		}
		else {
			animator.SetBool("isEnabled", false);
		}
	}

	public static void AddObjectif()
	{
		nbobjectif++;
	}

	public static void PickObjectif()
	{
		nbobjectif--;

	}

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		effect.isChangingSomething = true;
		if (nbobjectif<=0) {
			effect.isWinner = true;
		}
		return effect;
	}

}
