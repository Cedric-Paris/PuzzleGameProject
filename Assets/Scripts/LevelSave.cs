using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/// <summary>
/// Contains all the methods to save and load all of the elements of a level, the elements contained in the TileMap.
/// </summary>
public class LevelSave {

	/// <summary>
	/// The path of the directory containing the level saves.
	/// </summary>
	private static string pathLevelSaves = Application.persistentDataPath+"/Levels";

	/// <summary>
	/// The level saves extension.
	/// </summary>
	private static string levelSavesExtension= ".lvls";


	/// <summary>
	/// Static constructor is called at most one time, before any instance constructor is invoked or member is accessed.
	/// Checks if the saves directory exists, creates it if not.
	/// </summary>
	static LevelSave()
	{
		if (Directory.Exists (pathLevelSaves) == false)
			Directory.CreateDirectory(pathLevelSaves);
	}
	
	/// <summary>
	/// Saves in a file the tile map of the scene using the serializable object TileMapSave.
	/// </summary>
	/// <param name="fileName">Name of the file where the level will be save.</param>
	/// <param name="debug">If set to <c>true</c> display the list of all the elements saved in the console.</param>
	public static void SaveTileMap(string fileName, bool debug=false){
		TileMapSave tSave = new TileMapSave();
		GameObject[] tilemap=GameObject.FindGameObjectsWithTag("TileMap");
		foreach (Transform child in tilemap[0].transform)
			tSave.addSquare(child);

		Save(tSave, fileName+levelSavesExtension);
		
		//Debug
		if (debug) {
			string texte="ElementList saved in "+pathLevelSaves+"/"+fileName+levelSavesExtension+": ";
			foreach(var square in tSave.squareList) { texte+=square.Value+" - "+square.Key.getVector3()+";   "; }
			Debug.Log(texte);
		}
	}

	/// <summary>
	/// Read a level in a file and create the elements in it with the SquareInstanciation méthode
	/// </summary>
	/// <param name="fileName">File name.</param>
	/// <param name="debug">If set to <c>true</c> display the list of all the elements loaded in the console</param>
	public static void LoadTileMap(string fileName, bool debug=false){

		
		TileMapSave tLoad = (TileMapSave) Load(fileName+levelSavesExtension);

		if (debug) {
			string texte="ElementList that will be load from "+pathLevelSaves+fileName+levelSavesExtension+": ";
			foreach(var square in tLoad.squareList) { texte+=square.Value+" - "+square.Key.getVector3()+";   " ; }
			Debug.Log(texte);
		}

		GameObject tileMap = GameObject.FindGameObjectsWithTag("TileMap")[0];
		foreach (Transform child in tileMap.transform) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (var square in tLoad.squareList){
			SquareInstanciation(square, tileMap);
		}
		if (debug) {
			Debug.Log("Successfuly loaded");
		}
	}


	/// <summary>
	/// Instanciate the square passed in argument, and the element in the square if there's one.
	/// </summary>
	/// <param name="square">Square, represented with a vector3 for the position, and a string for the type of the square, which can contain 2 elements separedby a "|".</param>
	/// <param name="parent">The parent element of the square.</param>
	private static void SquareInstanciation(KeyValuePair<Vector3Save, string> square, GameObject parent){
		string[] nom=square.Value.Split(new string[] {"|"}, StringSplitOptions.None);
		if (nom.Count()==1){
			GameObject squarePrefab = (GameObject) Resources.Load(nom[0], typeof(GameObject));
			GameObject squareInstance = (GameObject) GameObject.Instantiate(squarePrefab, square.Key.getVector3(), Quaternion.identity);
			squareInstance.transform.SetParent(parent.transform);
			squareInstance.name = squarePrefab.name;
		}
		else{
			GameObject squarePrefab = (GameObject) Resources.Load(nom[0], typeof(GameObject));
			GameObject elementPrefab = (GameObject) Resources.Load(nom[1], typeof(GameObject));
			GameObject squareInstance = (GameObject) GameObject.Instantiate(squarePrefab, square.Key.getVector3(), Quaternion.identity);
			GameObject elementInstance = (GameObject) GameObject.Instantiate(elementPrefab, square.Key.getVector3(), Quaternion.identity);
			squareInstance.transform.SetParent(parent.transform);
			squareInstance.name = squarePrefab.name;
			elementInstance.transform.SetParent(squareInstance.transform);
			elementInstance.name = elementPrefab.name;
			
			Square squareScript = squareInstance.GetComponent<Square>();
			squareScript.squareElement = (Element) elementInstance.GetComponent<Element>();
		}
	}


	/// <summary>
	/// Save the specified entity in a file created with the fileName passed in argument.
	/// </summary>
	/// <param name="entity">Entity, must be serializable.</param>
	/// <param name="fileName">File name.</param>
	public static void Save(object entity, string fileName)
	{

			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Create(pathLevelSaves + "/" + fileName);
			formatter.Serialize(stream, entity);
			stream.Close();
	}
		
	/// <summary>
	/// Gets the list of the levels names.
	/// </summary>
	/// <returns>A String List of the levels</returns>
	public static List<string> GetLevels(){
		List<string> levelList = new List<String>();
		foreach (string file in System.IO.Directory.GetFiles(pathLevelSaves))
		{
			if (file.EndsWith(levelSavesExtension)){
				string fileName=file.Split('\\').Last();
				levelList.Add(fileName.Remove(fileName.Length-levelSavesExtension.Length));
			}
		}
		return levelList;
	}

	/// <summary>
	/// Load the file at the specified fileName.
	/// </summary>
	/// <param name="fileName">File name.</param>
	public static object Load(string fileName)
	{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = File.Open(pathLevelSaves + "/" + fileName, FileMode.Open);
			var entity = formatter.Deserialize(stream);
			stream.Close();
			return entity;
	}
}
