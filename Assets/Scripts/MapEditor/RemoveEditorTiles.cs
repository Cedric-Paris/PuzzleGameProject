using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RemoveEditorTiles : MonoBehaviour {


	private PutEditorTiles pEditTilesAssociated;
	private List<Vector3> baseTilesAlreadySet;
	private List<Vector3> elementTilesAlreadySet = new List<Vector3>();


	void Update () {
		Camera c = Camera.main;
		Vector3 worldPos;
		Vector3 tt = new Vector3();
		foreach (Touch t in Input.touches) {
			worldPos = c.ScreenToWorldPoint(t.position);
			worldPos.x = CalculDemiLePlusProche(worldPos.x);
			worldPos.y = CalculDemiLePlusProche(worldPos.y);
			worldPos.z = 0;
			foreach(Collider2D col in Physics2D.OverlapCircleAll(worldPos, 1.5f))
			{
				if(col.gameObject.transform.position == worldPos)
				{
					if(col.gameObject.GetComponent<Square>() != null)
						baseTilesAlreadySet.Remove(worldPos);
					else
						elementTilesAlreadySet.Remove(worldPos);
					Destroy(col.gameObject);
					break;
				}
			}
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		if(pEditTilesAssociated == null)
		{
			pEditTilesAssociated = GetComponent<PutEditorTiles>();
			if(pEditTilesAssociated == null)
				this.enabled = false;
		}
		baseTilesAlreadySet = pEditTilesAssociated.getBaseTiles();
		elementTilesAlreadySet = pEditTilesAssociated.getElementTiles();
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
