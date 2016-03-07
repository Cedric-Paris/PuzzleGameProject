using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public delegate void AnimationFinished();
	public event AnimationFinished onAnimationFinished;

	public void OnAnimationFinished()
	{
		if(onAnimationFinished != null)
			onAnimationFinished();
		Destroy(gameObject);
	}
}
