using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
	
	public void LoadScene(string scene)
	{
        AssuresMainMenuBackgroundStays();
        AssuresSoundStays();
		Application.LoadLevel(scene);
	}

    private void AssuresMainMenuBackgroundStays()
    {
        MainMenuBackground mmb = GameObject.FindObjectOfType<MainMenuBackground>();
        if (mmb != null)
            mmb.isFirstOne = true;
    }

    private void AssuresSoundStays()
    {
        SoundControlComponent scc = GameObject.FindObjectOfType<SoundControlComponent>();
        if (scc != null)
            scc.isFirstOne = true;
    }
}
