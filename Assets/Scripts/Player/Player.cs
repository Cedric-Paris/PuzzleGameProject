using UnityEngine;
using System.Collections;

/// <summary>
/// Class that represent graphically the player in the scene.
/// </summary>
public class Player : MonoBehaviour {

	/// <summary>
	/// Animator component associated to the Player.
	/// </summary>
	private Animator animator;
	/// <summary>
	/// PlayerMovementController that manage the movements of this player.
	/// </summary>
	private PlayerMovementController controller;
	/// <summary>
	/// The GameObject used when the player explode.
	/// </summary>
	public GameObject explosion;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Initializes some attributes.
	/// </summary>
    void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("AnimState", 1);
		controller = GetComponent<PlayerMovementController>();
		controller.onPlayerDirectionChanging += OnDirectionChange;
		controller.playerAssociated = this;
		setAnimation (controller.CurrentDirection.animationCode);
    }

	/// <summary>
	/// Call when the player direction is changing.
	/// </summary>
	/// <param name="dir">Dir.</param>
	private void OnDirectionChange(DirectionProperties dir)
	{
		setAnimation (dir.animationCode);
	}

	/// <summary>
	/// Explode this instance.
	/// </summary>
	public void Explode()
	{
		Explosion e = ((GameObject)Instantiate(explosion, this.transform.position, Quaternion.identity) ).GetComponent<Explosion>();
		e.onAnimationFinished += () => SceneLevelManager.main.ReloadCurrentScene();
		Destroy(this.gameObject);
	}

	/// <summary>
	/// Set the animation code.
	/// </summary>
	/// <param name="anim">Animation.</param>
	public void setAnimation(int anim){
		animator.SetInteger ("AnimState", anim);
	}
}
