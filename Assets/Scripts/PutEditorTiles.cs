using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PutEditorTiles : MonoBehaviour {

	private List<Vector3> baseTilesAlreadySet = new List<Vector3>();
	private List<Vector3> elementTilesAlreadySet = new List<Vector3>();

	public float xMaximumEnPartantDroite;//devra etre géré tout seul plus tard


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

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
