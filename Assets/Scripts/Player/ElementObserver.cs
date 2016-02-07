using UnityEngine;
using System.Collections;

/// <summary>
/// Detects elements encountered by the collider associated to the GameObject which contains this ElementObserver.
/// </summary>
public class ElementObserver : MonoBehaviour {

	/// <summary>
	/// The Element detected. When it's set, the <see cref="isTreated"/> attribute is set to false.
	/// </summary>
	/// <value>The element detected.</value>
	public Element ElementDetected
	{
		get { return mElementDetected; }
		set
		{
			mElementDetected = value;
			isTreated = false;
		}
	}
	/// <summary>
	/// See <see cref="ElementDetected"/>
	/// </summary>
	private Element mElementDetected;

	/// <summary>
	/// Indicates whether the element detected (<see cref="ElementDetected"/>) was treated.
	/// </summary>
	public bool isTreated;

	/// <summary>
	/// Called when the collider associated encounters another collider.
	/// Check if the object encountered is an element, if it is the case, it is put in <see cref="ElementDetected"/>.
	/// </summary>
	/// <param name="other">The collider encountered.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		Component com = other.gameObject.GetComponent<Element>();
		Element element = com as Element;
		if (element != null)
			ElementDetected = element;
	}
}
