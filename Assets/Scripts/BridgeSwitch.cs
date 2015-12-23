using UnityEngine;
using System.Collections;

public class BridgeSwitch : Special {

	private Animator animator;
	public ArrayList lBridge = new ArrayList ();
	public ArrayList lSwitch = new ArrayList ();

	// Use this for initialization
	void Start () {
		lBridge.AddRange( (Bridge[]) Object.FindObjectsOfType (typeof(Bridge)) ); // AddRange transforme le Bridge[] en ArrayList. Object.FindObjectsOfType nous retourne tous les objets
		lSwitch.AddRange( (BridgeSwitch[])Object.FindObjectsOfType (typeof(BridgeSwitch)) ); // de la Scene implémentant le script Bridge.cs
		animator = GetComponent<Animator> ();
	}


	public override EffectTransformation Effect (bool isTreated = false) {
		bool StateToBecome;
		if (isTreated == false)
			return new EffectTransformation(false);
		Debug.Log ("Changement de position des ponts et des interrupteurs.");
		foreach (Bridge b in lBridge)
			b.ChangePosition ();
		foreach (BridgeSwitch s in lSwitch)
			s.ChangeColor ();
		return new EffectTransformation(false);
	}

	public void ChangeColor()
	{
		animator.SetBool ("Active", !animator.GetBool ("Active"));
	}
}
