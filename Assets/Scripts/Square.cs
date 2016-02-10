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

	public void CheckElementAroundIfNull()
	{
		if(squareElement != null)
			return;
		foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position, 0.15f)) 
		{
			if( (squareElement = col.gameObject.GetComponent<Element>()) != null)
			{
				squareElement.transform.SetParent(this.transform);
				return;
			}
		}
	}

}
