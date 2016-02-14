using UnityEngine;
using System.Collections;

public class GlobalMultilingComponent : MonoBehaviour {

	private GlobalMultiling globalMultilingInstance;
	private static bool alreadyExistInScene = false;

	void Awake()
	{
		globalMultilingInstance = new GlobalMultiling();
	}

	void Start()
	{
		globalMultilingInstance.translateAll();
		DontDestroyOnLoad(this.gameObject);
		alreadyExistInScene = true;
	}
	
	void OnLevelWasLoaded(int level)
	{
		if(level == 0 && alreadyExistInScene)
		{
			Destroy(this.gameObject);
			alreadyExistInScene = false;
			return;
		}
		globalMultilingInstance.translateAll();
	}
}
