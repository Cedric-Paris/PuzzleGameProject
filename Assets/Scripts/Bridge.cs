using UnityEngine;
using System.Collections;

public class Bridge : Element {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool isDown;

	public void ChangePosition() {
		if (this.isDown)
			this.isDown = false;
		else
			this.isDown = true;
		return;
	}

	public override EffectTransformation Effect () {
		EffectTransformation effect = new EffectTransformation ();
		if (!this.isDown) {
			Debug.Log ("Ceci est un obstacle, vous ne pouvez avancer");
			effect.isObstacle = true;
		} else
			return effect;
		return effect;
	}
}
