using UnityEngine;
using System.Collections;

public class BridgeSwitch : Special {

	public ArrayList<Bridge> lBrige;

	// Use this for initialization
	void Start () {

	}

	public EffectTransformation Effect()//ici element sans effet
	{
		Debug.Log("Changement de positions des ponts");
		foreach ( var i in lBrige)
			i.ChangePosition();
		return new EffectTransformation();
	}
}
