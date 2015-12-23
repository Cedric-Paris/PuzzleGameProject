using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator animator;
	private PlayerMovementController controller;
	public GameObject explosion;

    void Start () {
		animator = GetComponent<Animator> ();
		animator.SetInteger ("AnimState", 1);
		controller = GetComponent<PlayerMovementController>();
		controller.onPlayerDirectionChanging += OnDirectionChange;
		controller.playerAssociated = this;
		setAnimation (controller.CurrentDirection.animationCode);
    }

	private void OnDirectionChange(DirectionProperties dir)
	{
		setAnimation (dir.animationCode);
	}

	public void Explode()
	{
		Debug.Log ("Explode");
		Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy(this.gameObject);
	}

	public void setAnimation(int anim){
		animator.SetInteger ("AnimState", anim);
	}
}
