using UnityEngine;
using System.Collections;

/// <summary>
/// Element which represents a Bridge that can be in an open or closed state.
/// </summary>
public class Bridge : SpecialElement {

	/// <summary>
	/// Animator component associated to the Bridge.
	/// </summary>
	private Animator animator;
	/// <summary>
	/// Indicate if the bridge is down (closed).
	/// </summary>
	public bool isDown;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Initializes some attributes.
	/// </summary>
	void Start () {
		animator = GetComponent<Animator> ();
		if(!this.isDown)
			animator.SetBool ("IsOpened", true);
	}

	/// <summary>
	/// Change the bridge position (Opened / Closed).
	/// </summary>
	public void ChangePosition() {
		isDown = !isDown;
		animator.SetBool ("IsOpened", !animator.GetBool ("IsOpened"));
	}

	/// <summary>
	/// Element Effect. An EffectTransformation object type is returned with the modification applied by the effect.
	/// If the bridge is down (closed), the EffectTransformation object indicates that the element is water.
	/// If the bridge is up (opened), there is no effect.
	/// </summary>
	/// <param name="isTreated">true = the element is treated definitively. / false = the element effect may be requested again.</param>
	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation();
		if (!this.isDown) {
			effect.isWater = true;
			return effect;
		}
		return new EffectTransformation(false);
	}
}
