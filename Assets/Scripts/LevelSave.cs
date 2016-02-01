using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;
using System.Collections.Generic;

public class LevelSave : MonoBehaviour{

		
	void Start(){
		//SaveTileMap("TileMap");
		//LoadTileMap("TileMap");
	}

	public static void SaveTileMap(string fileName, bool debug=false){
		TileMapSave tSave = new TileMapSave();
		GameObject[] tilemap=GameObject.FindGameObjectsWithTag("TileMap");
		foreach (Transform child in tilemap[0].transform) { tSave.addSquare(child); }

		Save(tSave, fileName);

		//Debug
		if (debug) {
			string texte="ElementList: ";
			foreach(var square in tSave.squareList) { texte+=square.Value+" - "+square.Key.getVector3()+"; "; }
			Debug.Log(texte);
		}
	}

	public static void LoadTileMap(string fileName, bool debug=false){

		
		TileMapSave tLoad = (TileMapSave) Load(fileName);

		if (debug) {
			string texte="ElementList: ";
			foreach(var square in tLoad.squareList) { texte+=square.Value+" - "+square.Key.getVector3()+"; " ; }
		}

		var oldTileMap = GameObject.FindGameObjectsWithTag("TileMap");
		Destroy(oldTileMap[0]);
		GameObject tileMap = new GameObject();
		tileMap.name = "TileMap";
		tileMap.tag = "TileMap";

		foreach (var square in tLoad.squareList){
			SquareInstanciation(square, tileMap);
		}
	}

	private static void SquareInstanciation(KeyValuePair<Vector3Save, string> square, GameObject parent){
		string[] nom=square.Value.Split(new string[] {"|"}, StringSplitOptions.None);
		if (nom.Count()==1){
			GameObject squarePrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Prefab/"+nom[0]+".prefab", typeof(GameObject));
			((GameObject) Instantiate(squarePrefab, square.Key.getVector3(), Quaternion.identity)).transform.SetParent(parent.transform);
		}
		else{
			GameObject squarePrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Prefab/"+nom[0]+".prefab", typeof(GameObject));
			GameObject elementPrefab = (GameObject) AssetDatabase.LoadAssetAtPath("Assets/Prefab/"+nom[1]+".prefab", typeof(GameObject));
			GameObject squareInstance = (GameObject) Instantiate(squarePrefab, square.Key.getVector3(), Quaternion.identity);
			GameObject elementInstance = (GameObject) Instantiate(elementPrefab, square.Key.getVector3(), Quaternion.identity);
			squareInstance.transform.SetParent(parent.transform);
			elementInstance.transform.SetParent(squareInstance.transform);

			Square squareScript = squareInstance.GetComponent<Square>();
			squareScript.squareElement = (Element) elementInstance.GetComponent<Element>();
		}
	}



	public static void Save(object entity, string fileName)
	{

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Create(Application.persistentDataPath + "/" + fileName);
			formatter.Serialize(stream, entity);
			stream.Close();
	}
		
	public static object Load(string fileName)
	{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
			var entity = formatter.Deserialize(stream);
			stream.Close();
			return entity;
	}
}
