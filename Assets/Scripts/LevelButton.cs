using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	public int levelNumber;
	public string levelScoreKey;
	public Button associatedButton;
	public Image imageStars;
	public GameObject starsPanel;
	public Sprite stars0;
	public Sprite stars1;
	public Sprite stars2;
	public Sprite stars3;

	void Start()
	{
		if(PlayerPrefs.GetInt("Last Unlocked Level") < levelNumber)
		{
			associatedButton.interactable = false;
			return;
		}
		if(PlayerPrefs.HasKey(levelScoreKey))
		{
			switch(PlayerPrefs.GetInt(levelScoreKey))
			{
			case 0:
				imageStars.sprite = stars0;
				break;
			case 1:
				imageStars.sprite = stars1;
				break;
			case 2:
				imageStars.sprite = stars2;
				break;
			default:
				imageStars.sprite = stars3;
				break;
			}
			starsPanel.SetActive(true);
		}
	}

	public void ClickStartGame()
	{
		ProgressionSave.currentlevel = levelNumber;
	}
}
