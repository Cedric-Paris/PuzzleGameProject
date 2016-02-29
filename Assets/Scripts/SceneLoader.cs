using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
	
	public void loadScene(string scene)
	{
		Application.LoadLevel(scene);
	}
}
