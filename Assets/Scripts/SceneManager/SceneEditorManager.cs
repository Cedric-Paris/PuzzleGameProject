using UnityEngine;
using System.Collections;

public class SceneEditorManager : SceneManager {

	public override void LoadNextScene()
	{
		ReloadCurrentScene();
	}
	
	public override void ReloadCurrentScene()
	{
		Application.LoadLevel(currentScene);
	}
	
	public override void LoadPreviousScene()
	{
		SceneReloader.needToBeReload = true;
		Application.LoadLevel(previousScene);
		Debug.Log ("Temp");
	}
}
