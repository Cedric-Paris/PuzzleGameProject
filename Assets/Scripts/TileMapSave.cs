using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// Serializable entity which can contain each square of a tileMap
/// </summary>
[Serializable]
public class TileMapSave{
	

	/// <summary>
	/// The square list. Vector3Save is the position of the element, string is his name (also the name of his prefab). A square containing an element will have a string value as 'Square|Element'
	/// </summary>
	public Dictionary<Vector3Save, string> squareList = new Dictionary<Vector3Save, string>();

	/// <summary>
	/// Adds a square to the squareList.
	/// </summary>
	/// <param name="a">The square that will be added</param>
	public void addSquare(Transform square){
		string test="";
		string nom=square.name;
		if (square.transform.childCount>0)
			nom+="|"+square.transform.GetChild(0).name;
		Vector3Save vSave= Vector3Save.getVector3Save(square.position);
		if (!squareList.TryGetValue(vSave,out test)){
			squareList.Add(vSave, nom);
		}
	}




}
