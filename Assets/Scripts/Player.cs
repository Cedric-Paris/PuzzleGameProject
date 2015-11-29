using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public readonly static Vector2 GO_UP = new Vector2(0, 1);
    public readonly static Vector2 GO_DOWN = new Vector2(0, -1);
    public readonly static Vector2 GO_RIGHT = new Vector2(1, 0);
    public readonly static Vector2 GO_LEFT = new Vector2(-1, 0);

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
