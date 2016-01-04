using UnityEngine;
using System.Collections;

public class ElementOneUse : Element {

	public Element elementBase;

	public override EffectTransformation Effect(bool isTreated = false)
	{
		if (isTreated == true) {
			Destroy (gameObject);
			return elementBase.Effect (true);
		}
		return elementBase.Effect (false);
	}
}
