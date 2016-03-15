using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PositionData : MonoBehaviour {

	private static List<PositionData> listePositions = new List<PositionData>();

	private float positionXMin;
	private float positionXMax;
	private float positionYMin;
	private float positionYMax;

	public bool needToBeUpdated;

	void Start()
	{
		initializeValues();
		listePositions.Add(this);
	}

	void Update()
	{
		if(!needToBeUpdated)
			return;
		initializeValues();
	}

	public static void UpdatePositions()
	{
		foreach(PositionData p in listePositions)
			p.initializeValues();
	}

	private void initializeValues()
	{
		RectTransform rect = GetComponent<RectTransform>();
		Vector3 rightUpper = new Vector3(Screen.width * rect.anchorMax.x, Screen.height * rect.anchorMax.y, 0);
		rightUpper = Camera.main.ScreenToWorldPoint(rightUpper);
		positionXMax = rightUpper.x;
		positionYMax = rightUpper.y;
		Vector3 leftLower = new Vector3(Screen.width * rect.anchorMin.x, Screen.height * rect.anchorMin.y, 0);
		leftLower = Camera.main.ScreenToWorldPoint(leftLower);
		positionXMin = leftLower.x;
		positionYMin = leftLower.y;
	}

	public static bool IsPositionAllowed(Vector3 position)
	{
		foreach(PositionData p in listePositions)
		{
			if(p.IsPositionIn(position))
			   return false;
		}
		return true;
	}

	private bool IsPositionIn(Vector3 position)
	{
		if(position.x >= CalculDemiLePlusProche(positionXMin) && position.x <= CalculDemiLePlusProche(positionXMax)
			   && position.y >= CalculDemiLePlusProche(positionYMin) && position.y <= CalculDemiLePlusProche(positionYMax))
			return true;
		return false;
	}

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}

	void OnLevelWasLoaded(int level)
	{
		listePositions = new List<PositionData>();
	}

}
