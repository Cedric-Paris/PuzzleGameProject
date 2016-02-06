using UnityEngine;
using System.Collections;

public class SidePanelManager : MonoBehaviour {

	private Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
		if(anim == null)
			Debug.LogError("Error - no animator");
	}

	public void SetPanelState(bool value)
	{
		anim.SetBool("IsOpen",value);
	}
}
