using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {

	public Element squareElement;
	
	void Start () {
		if (squareElement != null) {
			if (!squareElement.isActiveAndEnabled)
				squareElement=Instantiate(squareElement);
			squareElement.transform.position = transform.position;
		}
	}

}
