using UnityEngine;
using System.Collections;

/// <summary>
/// Tempory class to save the level progression.
/// </summary>
public class TestSauvegarde2 : MonoBehaviour
{
	/// <summary>
	/// The currentlevel.
	/// </summary>
	public int currentlevel;


	/// <summary>
	/// .
	/// </summary>
	void Start ()
	{	
		Debug.Log ("Ceci est un test de Sauvegarde");
		Debug.Log (PlayerPrefs.GetInt("LevelComplete"));
		if (PlayerPrefs.GetInt ("Level Complete") > 0) {
			currentlevel = PlayerPrefs.GetInt ("Level Complete");
		} else {
			currentlevel = 0;
		}
		Application.LoadLevel (currentlevel);
		if (currentlevel < 10) {
			currentlevel= currentlevel + 1;
			PlayerPrefs.SetInt ("Level Complete", currentlevel);
		} else {
			print ("This is the End!!!");
		}

	}
	
	void Update ()
	{
	
	}
}
