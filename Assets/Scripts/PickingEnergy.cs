using UnityEngine;
using System.Collections;

public class PickingEnergy : Objectif {

	private Energy energy;
	// Use this for initialization
	void Start () {
		energy = GameObject.FindObjectOfType<Energy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isEnergy = true;
		energy.AddEnergy ();
		return effect;
	}

}
