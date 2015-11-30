using UnityEngine;
using System.Collections;

public class FinalCase : Objectif {

	public static int objectifIterator;
	public int totalIterator;


	// Use this for initialization
	void Start () {
		PickingObjectif[] tab;
		tab = this.GetComponentsInParent<PickingObjectif>();
		totalIterator = tab.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		if (objectifIterator == totalIterator) {
			effect.isWinner = true;
		}
		return effect;
	}

}
