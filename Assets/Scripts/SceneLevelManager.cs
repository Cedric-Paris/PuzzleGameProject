using UnityEngine;
using System.Collections;

public class SceneLevelManager : MonoBehaviour {

	public static SceneLevelManager main { get; private set;}

	public string previousScene;
	public string nextScene;
	public string currentScene;

	void Start()
	{
		main = this;
	}

	public void LoadNextScene()
	{
		if(nextScene == null || nextScene == "")
			return;
		Application.LoadLevel(nextScene);
	}

	public void ReloadCurrentScene()
	{
		if(currentScene == null || currentScene == "")
			return;
		Application.LoadLevel(currentScene);
	}

	public void LoadPreviousScene()
	{
		if(previousScene == null || previousScene == "")
			return;
		Application.LoadLevel(previousScene);
	}
}
