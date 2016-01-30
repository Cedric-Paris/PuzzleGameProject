using UnityEngine;
using System.Collections;

/// <summary>
/// Used to associate a GameObject sprite to a UI element in the Map Editor menu.
/// </summary>
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
		rTransform.localScale = new Vector3 (sRenderer.sprite.pixelsPerUnit,sRenderer.sprite.pixelsPerUnit);
	}
}
