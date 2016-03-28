using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneReloader : MonoBehaviour {

	public static bool needToBeReload;

	void Start ()
	{
		if(!needToBeReload)
			return;
		needToBeReload = false;
		Debug.Log ("loading");
		Dictionary<string, int> actions = LevelSave.LoadTileMap(EditorPlayButton.FILE_PATH);
		this.gameObject.GetComponent<PutEditorTiles>().RefreshPosition();
	}
}
