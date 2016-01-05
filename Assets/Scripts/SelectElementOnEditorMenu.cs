using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectElementOnEditorMenu : MonoBehaviour {

	public static GameObject selectedObject;
	public static Button currentButton;
	public static bool enableTileSet = true;//Si false on ne placera pas les tuiles sur la map

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
