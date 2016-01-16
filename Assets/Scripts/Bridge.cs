using UnityEngine;
using System.Collections;

public class Bridge : SpecialElement {

	private Animator animator;
	public bool isDown;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		if(!this.isDown)
			animator.SetBool ("IsOpened", true);
	}

	public void ChangePosition() {
		isDown = !isDown;
		animator.SetBool ("IsOpened", !animator.GetBool ("IsOpened"));
	}

	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation();
		if (!this.isDown) {
			effect.isWater = true;
			return effect;
		}
		return new EffectTransformation(false);
	}
}
