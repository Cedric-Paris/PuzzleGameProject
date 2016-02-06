using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Start case of the player on the TileMap
/// </summary>
using System.Collections.Generic;


public class StartCase : Element {

	/// <summary>
	/// The entity (the player) that will be created by the method Play().
	/// </summary>
	public Player startPlayer;

	/// <summary>
	/// The direction that the player will start with, is changed each time the square is touched.
	/// </summary>
	public DirectionProperties startDirection;

	/// <summary>
	/// The animator of the Square, displaying the player and changing his direction.
	/// </summary>
	private Animator animator;

	/// <summary>
	/// The int value of the animation.
	/// </summary>
	private int anim;

	/// <summary>
	/// An array containing the directions that can take the player, in the order of the rotation
	/// </summary>
	private static DirectionProperties[] DirectionsArray = new[]{
		PlayerMovementController.GO_RIGHT,
		PlayerMovementController.GO_DOWN,
		PlayerMovementController.GO_LEFT,
		PlayerMovementController.GO_UP};

	/// <summary>
	/// Start this instance. Set the player direction to the right
	/// </summary>
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("animState", 0);
		startDirection = DirectionsArray[0];

	}

	/// <summary>
	/// Raises the mouse up event. The StartDirection is changed and the animation of the Square too
	/// </summary>
	public void OnMouseUp(){
		anim = (anim + 1) % 4;
		animator.SetInteger ("animState", anim);
		startDirection = DirectionsArray[anim];
	}

	/// <summary>
	/// Create the player with the startDirection, and destroy the StartCase
	/// </summary>
	public void Play() {
		startPlayer=(Player)Instantiate(startPlayer, this.gameObject.transform.position, Quaternion.identity);
		startPlayer.GetComponent<PlayerMovementController>().CurrentDirection = startDirection;
		Destroy (this.gameObject);
	}
}
