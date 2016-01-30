using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manage the positioning of elements on the scene in the Map Editor
/// Detect where the user clicked and places the selected item on the editor menu to the correct position
/// </summary>
public class PutEditorTiles : MonoBehaviour {

	/// <summary>
	/// List of Vector3 corresponding to the position of all the squares placed on the scene
	/// </summary>
	private List<Vector3> baseTilesAlreadySet = new List<Vector3>();
	/// <summary>
	/// List of Vector3 corresponding to the position of all the elements placed on the scene
	/// </summary>
	private List<Vector3> elementTilesAlreadySet = new List<Vector3>();

	public float xMaximumEnPartantDroite;//devra etre géré tout seul plus tard

	//TRAITER CAS BOUTON POUR PAS AFFICHER
	/// <summary>
	/// Called every frame, if the MonoBehaviour is enabled.
	/// Check the current touches on the screen and places the selected item on the editor menu on the scene at every touch position
	/// </summary>
	void Update () {
		if (SelectElementOnEditorMenu.selectedObject == null || !SelectElementOnEditorMenu.enableTileSet)
			return;
		Camera c = Camera.main;
		Vector3 worldPos;
		foreach (Touch t in Input.touches) {
			if(t.position.x > Screen.width - xMaximumEnPartantDroite)
				continue;
			worldPos = c.ScreenToWorldPoint(t.position);
			worldPos.x = CalculDemiLePlusProche(worldPos.x);
			worldPos.y = CalculDemiLePlusProche(worldPos.y);
			worldPos.z = 0;
			if(SelectElementOnEditorMenu.selectedObject.GetComponent<Square>() != null)
			{
				if(baseTilesAlreadySet.Contains(worldPos))
					continue;
				baseTilesAlreadySet.Add(worldPos);
			}
			else
			{
				if(elementTilesAlreadySet.Contains(worldPos))
					continue;
				elementTilesAlreadySet.Add(worldPos);
			}
			((GameObject)Instantiate(SelectElementOnEditorMenu.selectedObject, worldPos, Quaternion.identity)).transform.SetParent(this.gameObject.transform);
		}
	}

	/// <summary>
	/// Calculate the number in .5 nearest to the value passed as argument.
	/// </summary>
	/// <returns>The nearest number in 0.5</returns>
	/// <param name="value">value</param>
	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
