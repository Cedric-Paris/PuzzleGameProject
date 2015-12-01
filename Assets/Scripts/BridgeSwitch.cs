using UnityEngine;
using System.Collections;

public class BridgeSwitch : Special {
	

	// Use this for initialization
	void Start () {

	}

	public ArrayList lBridge = new ArrayList();

	public override EffectTransformation Effect (bool isTreated = false) {

		Debug.Log ("Changement de position des ponts");
		foreach (Bridge i in lBridge)
			i.ChangePosition ();
		return new EffectTransformation();
	}
}
