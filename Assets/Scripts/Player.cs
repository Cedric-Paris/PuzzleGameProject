using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private float speed = -0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(speed, 0));
	}
}
