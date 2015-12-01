using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private PlayerMovementController controller;

    void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("AnimState", 1);
		controller = GetComponent<PlayerMovementController>();
		controller.onPlayerDirectionChanging += OnDirectionChange;
    }

	private void OnDirectionChange(DirectionProperties dir)
	{
		animator.SetInteger ("AnimState", dir.animationCode);
	}

}
