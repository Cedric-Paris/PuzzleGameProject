using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

	public bool firstTime;
	public bool needToInverse;

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
			downArrowMenuButton.ElementCount = 1 + actions["UpArrow"];
			upArrowMenuButton.ElementCount = 1 + actions["DownArrow"];
		}
		else
		{
			downArrowMenuButton.ElementCount = 1 + actions["DownArrow"];
			upArrowMenuButton.ElementCount = 1 + actions["UpArrow"];
		}
		leftArrowMenuButton.ElementCount = 1+ actions["LeftArrow"];
		rightArrowMenuButton.ElementCount = 1 + actions["RightArrow"];

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

		upArrowMenuButton.DownElementCount ();
		downArrowMenuButton.DownElementCount ();
		leftArrowMenuButton.DownElementCount ();
		rightArrowMenuButton.DownElementCount ();

		if(firstTime)
		{
			firstTime = false;
			return;
		}

	}
}
