using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class EditorPlayButton : MonoBehaviour {

	private const string FILE_PATH = "PrivateLevels/TemporaryMapFromEditor";

	private bool isOnClick = false;


	public void OnClickPlay()
	{
		isOnClick = true;

		string path = Application.persistentDataPath+"/Levels/PrivateLevels";
		if (Directory.Exists(path) == false)
			Directory.CreateDirectory(path);

		LevelSave.SaveTileMap(FILE_PATH, null);
		Application.LoadLevel("EmptySceneWithMenu");
	}

	void OnLevelWasLoaded(int level)
	{
		if(!isOnClick)
			return;
		isOnClick = false;
		Dictionary<string, int> actions = LevelSave.LoadTileMap(FILE_PATH);
		//Mettre actions dans menu
	}
}
