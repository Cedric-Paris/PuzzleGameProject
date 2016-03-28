using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class EditorPlayButton : MonoBehaviour {

	public ActionsPanel actionPanel;

	public const string FILE_PATH = "PrivateLevels/TemporaryMapFromEditor";

	private bool isOnClick = false;

	public void OnClickPlay()
	{
		isOnClick = true;

		string path = Application.persistentDataPath+"/Levels/PrivateLevels";
		if (Directory.Exists(path) == false)
			Directory.CreateDirectory(path);

		LevelSave.SaveTileMap(FILE_PATH, actionPanel.actions);
		GameObject.Find("TileMapEditor").tag = "RejectTileMap";
		Application.LoadLevel("EmptySceneWithMenu");
	}
	
}
