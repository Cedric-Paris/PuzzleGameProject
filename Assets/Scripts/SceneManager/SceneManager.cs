using UnityEngine;
using System.Collections;

public abstract class SceneManager : MonoBehaviour {

	public static SceneManager main { get; private set;}
	
	public string previousScene;
	public string nextScene;
	public string currentScene;

	void Start()
	{
		main = this;
	}

	public abstract void LoadNextScene();

	public abstract void ReloadCurrentScene();

	public abstract void LoadPreviousScene();
}
