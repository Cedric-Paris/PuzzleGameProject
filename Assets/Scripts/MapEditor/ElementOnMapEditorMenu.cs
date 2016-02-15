using UnityEngine;
using System.Collections;

/// <summary>
/// Used to associate a GameObject sprite to a UI element in the Map Editor menu.
/// </summary>
using UnityEngine.UI;


public class ElementOnMapEditorMenu : MonoBehaviour {

	/// <summary>
	/// Source GameObject for the sprite.
	/// </summary>
	public GameObject gObject;
	/// <summary>
	/// SpriteRenderer of the gameObject that will receive the sprite.
	/// </summary>
	public SpriteRenderer sRenderer;
	/// <summary>
	/// RectTransform of the gameObject that will receive the sprite.
	/// </summary>
	public RectTransform rTransform;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Retrieve the sprite of the gameObject (gObject) and applies it to SpriteRenderer sRenderer.
	/// </summary>
	void Start () {
		if (gObject == null || sRenderer == null)
			return;
		SpriteRenderer spriteRend = gObject.GetComponent<SpriteRenderer> ();
		sRenderer.sprite = spriteRend.sprite;
		float val = Screen.width * 0.15f;
		GetComponentInParent<LayoutElement>().preferredHeight = val;
		rTransform.localScale = new Vector3 (val, val);
	}
}
