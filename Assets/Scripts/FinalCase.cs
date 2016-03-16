using UnityEngine;
using System.Collections;

/// <summary>
/// Final Square of a level, the player must pass on it after picking all the objectives to win.
/// </summary>
public class FinalCase : Objectif {

	/// <summary>
	/// The number of Objectifs to pick to activate the Square
	/// </summary>
	public static int nbobjectif = 0;

	/// <summary>
	/// The animator.
	/// </summary>
	private Animator animator;


	/// <summary>
	/// Start the instance. The square start activated
	/// </summary>
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetBool("isEnabled", true);
	}
	
	/// <summary>
	/// If there is some objectives to pick, disable the square, else enable it
	/// </summary>
	void Update () {
		if(animator == null)
			return;
		if (nbobjectif <= 0) {
			animator.SetBool("isEnabled", true);
		}
		else {
			animator.SetBool("isEnabled", false);
		}
	}

	/// <summary>
	/// Increment the number of objectives to pick
	/// </summary>
	public static void AddObjectif()
	{
		nbobjectif++;
	}

	/// <summary>
	/// Decrement the number of objectives to pick
	/// </summary>
	public static void PickObjectif()
	{
		nbobjectif--;
	}

	/// <summary>
	/// Return the effect of the square. If there are no more objectives to pick, the player win
	/// </summary>
	public override EffectTransformation Effect (bool isTreated = false) {
		EffectTransformation effect = new EffectTransformation ();
		effect.isObjectif = true;
		if (nbobjectif<=0) {
			effect.isChangingSomething = true;
			effect.isWinner = true;
		}
		return effect;
	}

	void OnLevelWasLoaded(int level)
	{
		nbobjectif = 0;
	}

}
