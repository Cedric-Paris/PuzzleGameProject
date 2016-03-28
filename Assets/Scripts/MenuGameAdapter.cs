using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuGameAdapter : MonoBehaviour {

	public Button playButton;
	public UpArrowMenuButton upArrowMenuButton;
	public DownArrowMenuButton downArrowMenuButton;
	public LeftArrowMenuButton leftArrowMenuButton;
	public RightArrowMenuButton rightArrowMenuButton;
	public Text upArrowCount;
	public Text downArrowCount;
	public Text leftArrowCount;
	public Text rightArrowCount;
	public GameObject menuCanvas;

	public bool firstTime = true;
	public bool needToInverse = true;

	public void adaptMenu(StartCase startCase, Dictionary<string, int> actions)
	{
		Destroy(GameObject.Find("UIRightArrow(Clone)"));
		Destroy(GameObject.Find("UIDownArrow(Clone)"));
		Destroy(GameObject.Find("UILeftArrow(Clone)"));
		Destroy(GameObject.Find("UIUpArrow(Clone)"));
		playButton.onClick.RemoveAllListeners();
		if(startCase!=null)
		{
			playButton.onClick.AddListener( () => startCase.Play() );
		}
		if(needToInverse)
		{
			downArrowMenuButton.ElementCount = actions["UpArrow"];
			upArrowMenuButton.ElementCount = actions["DownArrow"];
		}
		else
		{
			downArrowMenuButton.ElementCount = actions["DownArrow"];
			upArrowMenuButton.ElementCount = actions["UpArrow"];
		}
		leftArrowMenuButton.ElementCount = actions["LeftArrow"];
		rightArrowMenuButton.ElementCount = actions["RightArrow"];

		if(needToInverse)
		{
			downArrowCount.text = "x "+actions["UpArrow"];
			upArrowCount.text = "x "+actions["DownArrow"];
		}
		else
		{
			downArrowCount.text = "x "+actions["DownArrow"];
			upArrowCount.text = "x "+actions["UpArrow"];
		}
		leftArrowCount.text = "x "+actions["LeftArrow"];
		rightArrowCount.text = "x "+actions["RightArrow"];
		if(firstTime)
		{
			firstTime = false;
			return;
		}
		menuCanvas.BroadcastMessage("Start");
	}
}
