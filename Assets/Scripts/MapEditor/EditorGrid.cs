using UnityEngine;
using System.Collections;

public class EditorGrid : MonoBehaviour {

	public Camera camAssociated;
	private Vector3 gridPosition;

	void Start ()
	{
		if (camAssociated == null)
			camAssociated = Camera.main;
		Update();
	}

	void Update ()
	{

		if (gridPosition == this.transform.position)
			return;
		this.transform.position = new Vector3 (CalculDemiLePlusProche (camAssociated.transform.position.x), CalculDemiLePlusProche (camAssociated.transform.position.y), this.transform.position.z);
	}

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
