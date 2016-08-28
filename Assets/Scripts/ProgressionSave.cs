using UnityEngine;
using System.Collections;

/// <summary>
/// Tempory class to save the level progression.
/// </summary>
public class ProgressionSave : MonoBehaviour
{

	public static int currentlevel = -1;

	void Awake()
	{
		if(! PlayerPrefs.HasKey("Last Unlocked Level"))
		{
			initialiseProgression();
		}
	}

	public static void LevelFinish(int score)
	{
		if(currentlevel < 0)
			return;
		PlayerPrefs.SetInt("ScoreLevel"+currentlevel, score);
		int lastLevelN = PlayerPrefs.GetInt("Last Unlocked Level");
		if(lastLevelN <= currentlevel)
		{
			PlayerPrefs.SetInt("Last Unlocked Level", currentlevel+1);
		}
	}

	private void initialiseProgression()
	{
		PlayerPrefs.SetInt("Last Unlocked Level", 1);
	}
}
