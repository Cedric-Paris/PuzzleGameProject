using UnityEngine;
using System.Collections;

/// <summary>
/// Represents an element and its effect.
/// An element is an entity that interacts with the player when he drive over:
/// it can influence his behavior (change of direction...), be picked up, be activated or cause his death.
/// </summary>
public class Element : MonoBehaviour {

	/// <summary>
	/// Element Effect. An EffectTransformation object type is returned with the modification applied by the effect.
	/// No effect in this case.
	/// </summary>
	/// <param name="isTreated">true = the element is treated definitively. / false = the element effect may be requested again.</param>
    public virtual EffectTransformation Effect(bool isTreated = false)//ici element sans effet
    {
        return new EffectTransformation();
    }

	public bool ElementCanBePlacedHere(Vector3 position)
	{
		bool squareFound = false;
		bool elementFound = false;
		foreach (Collider2D col in Physics2D.OverlapCircleAll(position, 0.15f)) 
		{
			if( col.gameObject.GetComponent<Element>() != null)
				elementFound = true;
		}
		position.z = position.z + 0.2f;
		foreach (Collider col in Physics.OverlapSphere(position, 0.15f))
		{
			if( col.gameObject.GetComponent<Square>() != null)
				squareFound = true;
		}
		if(elementFound || !squareFound)
		{
			return false;
		}
		return true;
	}

	public void CheckSquareAroundToAttach()
	{
		Square s;
		foreach (Collider2D col in Physics2D.OverlapCircleAll(this.transform.position, 0.15f)) 
		{
			if( (s = col.gameObject.GetComponent<Square>()) != null)// && s.squareElement == null)
			{
				s.squareElement = this;
				this.transform.SetParent(s.transform);
				return;
			}
		}
	}
}
