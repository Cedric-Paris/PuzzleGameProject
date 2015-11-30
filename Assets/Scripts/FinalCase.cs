using UnityEngine;
using System.Collections;

public class FinalCase : Objectif {

	public int objectifIterator;
	public int totalIterator;


	// Use this for initialization
	void Start () {
		ArrayList pickingObjfList = new ArrayList ();
		pickingObjfList = this.GetComponentInParent (PickingObjectif);
		totalIterator = pickingObjfList.Count ();
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
