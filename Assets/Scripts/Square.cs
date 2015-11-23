using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	public Element squareElement;

	// Use this for initialization
	void Start () {
		if (squareElement!=null)
			squareElement.transform.position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
