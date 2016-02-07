using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

/// <summary>
/// Draggable is the base interface for all GameObject that can be dragged.
/// It contains three method required to drag an object.
/// </summary>
public interface Draggable {

	/// <summary>
	/// Called when the mouse is down and move.
	/// </summary>
	void OnMouseDrag ();

	/// <summary>
	/// Called when the mouse is released.
	/// </summary>
	void OnMouseUp ();

	/// <summary>
	/// Called when the mouse drop the GameObject.
	/// </summary>
	void OnMouseDrop();

}
