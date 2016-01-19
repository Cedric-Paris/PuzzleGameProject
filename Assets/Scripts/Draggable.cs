using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public interface Draggable {

	void OnMouseDrag ();

	void OnMouseUp ();

	void OnMouseDrop();

}
