using UnityEngine;
using System.Collections;

public class FinalCase : Objectif {

	public static int objectifIterator;
	public int totalIterator;
	private Animator animator;


	// Use this for initialization
	void Start () {
		PickingObjectif[] tab;
		tab = this.GetComponentsInParent<PickingObjectif>();
		totalIterator = tab.Length;
		objectifIterator = 0;
		animator = GetComponent<Animator> ();
		if (objectifIterator == totalIterator) {
			animator.SetBool("isEnabled", true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PickObjectif()
	{
		objectifIterator++;
		if (objectifIterator == totalIterator) {
			animator.SetBool("isEnabled", true);
		}
	}

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		effect.isChangingSomething = true;
		if (objectifIterator == totalIterator) {
			effect.isWinner = true;
		}
		return effect;
	}

}
