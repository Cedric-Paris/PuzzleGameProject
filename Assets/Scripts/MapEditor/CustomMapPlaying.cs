using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomMapPlaying : MonoBehaviour {

	public MenuGameAdapter menuAdapter;

	void Start ()
	{
		FinalCase.nbobjectif = 0;
		Dictionary<string, int> actions = LevelSave.LoadTileMap(EditorPlayButton.FILE_PATH);
		GameObject tileMap = GameObject.FindGameObjectWithTag("TileMap");
		StartCase startCase = tileMap.GetComponentInChildren<StartCase>();
		menuAdapter.firstTime = false;
		menuAdapter.adaptMenu(startCase, actions);
	}
}
