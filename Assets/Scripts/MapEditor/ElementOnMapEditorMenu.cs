using UnityEngine;
using System.Collections;

public class ElementOnMapEditorMenu : MonoBehaviour {

	public GameObject gObject;
	public SpriteRenderer sRenderer;
	public RectTransform rTransform;
	// Use this for initialization
	void Start () {
		if (gObject == null || sRenderer == null)
			return;
		SpriteRenderer spriteRend = gObject.GetComponent<SpriteRenderer> ();
		sRenderer.sprite = spriteRend.sprite;
		rTransform.localScale = new Vector3 (sRenderer.sprite.pixelsPerUnit,sRenderer.sprite.pixelsPerUnit);
	}
}
