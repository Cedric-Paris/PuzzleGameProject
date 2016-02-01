using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class TileMapSave{
	public Dictionary<Vector3Save, string> squareList = new Dictionary<Vector3Save, string>();

	public void addSquare(Transform a){
		string test="";
		string nom=a.name;
		if (a.transform.childCount>0)
			nom+="|"+a.transform.GetChild(0).name;
		Vector3Save vSave= Vector3Save.getVector3Save(a.position);
		if (!squareList.TryGetValue(vSave,out test)){
			squareList.Add(vSave, nom);
		}
	}




}
