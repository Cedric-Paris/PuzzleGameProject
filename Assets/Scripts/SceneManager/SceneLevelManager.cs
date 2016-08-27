using UnityEngine;
using System.Collections;

public class SceneLevelManager : SceneManager {

	public override void LoadNextScene()
	{
		if(nextScene == null || nextScene == "")
			return;
		Application.LoadLevel(nextScene);
		ProgressionSave.currentlevel += 1;
	}

	public override void ReloadCurrentScene()
	{
		if(currentScene == null || currentScene == "")
			return;
		Application.LoadLevel(currentScene);
	}

	public override void LoadPreviousScene()
	{
		if(previousScene == null || previousScene == "")
			return;
		Application.LoadLevel(previousScene);
		ProgressionSave.currentlevel -= 1;
	}
}
