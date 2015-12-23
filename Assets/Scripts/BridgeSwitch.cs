using UnityEngine;
using System.Collections;

public class BridgeSwitch : Special {

	private Animator animator;
	public ArrayList lBridge = new ArrayList();
	public ArrayList lSwitch = new ArrayList();

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}


	public override EffectTransformation Effect (bool isTreated = false) {
		if (isTreated == false)
			return new EffectTransformation(false);
		Debug.Log ("Changement de position des ponts");
		foreach (Bridge i in lBridge)
			i.ChangePosition ();
		ChangeColor ();
		foreach (BridgeSwitch i in lSwitch)
			i.ChangeColor ();
		return new EffectTransformation(false);
	}

	public void ChangeColor()
	{
		animator.SetBool ("Active", !animator.GetBool ("Active"));
	}
}
