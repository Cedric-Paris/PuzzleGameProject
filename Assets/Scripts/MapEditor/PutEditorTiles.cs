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
	public List<Vector3> getBaseTiles() { return baseTilesAlreadySet; }
	/// <summary>
	/// List of Vector3 corresponding to the position of all the elements placed on the scene
	/// </summary>
	private List<Vector3> elementTilesAlreadySet = new List<Vector3>();
	public List<Vector3> getElementTiles() { return elementTilesAlreadySet; }
	
	/// <summary>
	/// Called every frame, if the MonoBehaviour is enabled.
	/// Check the current touches on the screen and places the selected item on the editor menu on the scene at every touch position
	/// </summary>
	void Update () {
		if (SelectElementOnEditorMenu.selectedObject == null || !SelectElementOnEditorMenu.enableTileSet)
			return;
		Camera c = Camera.main;
		Vector3 worldPos;
		GameObject instancePrefab;
		foreach (Touch t in Input.touches) {
			worldPos = c.ScreenToWorldPoint(t.position);
			worldPos.x = CalculDemiLePlusProche(worldPos.x);
			worldPos.y = CalculDemiLePlusProche(worldPos.y);
			worldPos.z = 0;
			if(!PositionData.IsPositionAllowed(worldPos))
			{
				continue;
			}
			if(SelectElementOnEditorMenu.selectedObject.GetComponent<Square>() != null)
			{
				if(baseTilesAlreadySet.Contains(worldPos))
					continue;
				baseTilesAlreadySet.Add(worldPos);
				instancePrefab = (GameObject)Instantiate(SelectElementOnEditorMenu.selectedObject, worldPos, Quaternion.identity);
				instancePrefab.AddComponent<BoxCollider2D>().size = new Vector2(0.2f, 0.2f);
				if(elementTilesAlreadySet.Contains(worldPos))
					instancePrefab.AddComponent<Square>().CheckElementAroundIfNull();
				instancePrefab.transform.SetParent(this.gameObject.transform);
			}
			else
			{
				if(elementTilesAlreadySet.Contains(worldPos))
					continue;
				elementTilesAlreadySet.Add(worldPos);
				instancePrefab = (GameObject)Instantiate(SelectElementOnEditorMenu.selectedObject, worldPos, Quaternion.identity);
				if(baseTilesAlreadySet.Contains(worldPos))
					instancePrefab.AddComponent<Element>().CheckSquareAroundToAttach();
				else
					instancePrefab.transform.SetParent(this.gameObject.transform);
			}
			instancePrefab.name = SelectElementOnEditorMenu.selectedObject.name;
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
