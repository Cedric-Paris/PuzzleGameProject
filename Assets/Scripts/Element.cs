﻿using UnityEngine;
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
}
