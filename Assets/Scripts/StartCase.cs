using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class StartCase : Element {

	public Player startPlayer;
	public DirectionProperties startDirection;
	private Animator animator;
	private int anim;


	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("animState", 0);
		startDirection = PlayerMovementController.GO_RIGHT;
	}

	public void OnMouseUp(){
		anim = (anim + 1) % 4;
		animator.SetInteger ("animState", anim);
	}

	public void Play() {
		startPlayer=(Player)Instantiate(startPlayer, this.gameObject.transform.position, Quaternion.identity);
		startPlayer.GetComponent<PlayerMovementController>().CurrentDirection = startDirection;
		Destroy (this.gameObject);
	}
}
