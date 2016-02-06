using UnityEngine;
using System.Collections;


/// <summary>
/// Pickable element. The objective must be picked by the player to enable the FinalCase
/// </summary>
public class PickingObjectif : Objectif {

	/// <summary>
	/// Increment the objectives count in the finalCase
	/// </summary>
	void Start () {
		FinalCase.AddObjectif();
	}


	/// <summary>
	/// return the effect of this element. If isTreated, decrement the objectives count in the finalCase
	/// </summary>
	/// <param name="isTreated">If set to <c>true</c> the objective is pick.</param>
	/// <returns> EffectTransformation that isObjectif</returns>
	public override EffectTransformation Effect(bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		if (isTreated) 
		{
			FinalCase.PickObjectif();
		}
		return effect;
	}
}
