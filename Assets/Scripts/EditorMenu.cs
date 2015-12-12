using UnityEngine;
using System.Collections;

public class EditorMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 r = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height));
		Debug.LogError (r.x);
	}
}
