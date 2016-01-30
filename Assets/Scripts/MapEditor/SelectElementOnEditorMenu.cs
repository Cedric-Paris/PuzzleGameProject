using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Manage the selection of items in the Map Editor menu.
/// </summary>
public class SelectElementOnEditorMenu : MonoBehaviour {

	/// <summary>
	/// GameObject associated to the last button clicked.
	/// </summary>
	public static GameObject selectedObject;
	/// <summary>
	/// Reference to the last button clicked.
	/// </summary>
	public static Button currentButton;
	/// <summary>
	/// Indicates whether the positioning of elements in the scene is enable.
	/// false = disable / true = enable
	/// </summary>
	public static bool enableTileSet = true;

	/// <summary>
	/// Called when an element is selected on the Map Editor menu.
	/// Update the selected object and the current button.
	/// </summary>
	/// <param name="buttonOnMenu">Button clicked.</param>
	public void OnButtonPressOnMenuEditor(Button buttonOnMenu)
	{
		ElementOnMapEditorMenu e = buttonOnMenu.GetComponentInChildren<ElementOnMapEditorMenu> ();
		selectedObject = e.gObject;
		if (currentButton != null)
			currentButton.image.color = new Color(255,255,255,0);
		buttonOnMenu.image.color = new Color(255,255,255,255);
		currentButton = buttonOnMenu;
	}
}
