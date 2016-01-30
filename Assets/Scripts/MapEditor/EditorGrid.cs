using UnityEngine;
using System.Collections;

/// <summary>
/// Maintains the editor grid has a correct position to keep the grid aligned with the square and elements on game.
/// </summary>
public class EditorGrid : MonoBehaviour {

	/// <summary>
	/// Camera associated to the Grid.
	/// </summary>
	public Camera camAssociated;
	/// <summary>
	/// Grid position at the end of last Update ().
	/// </summary>
	private Vector3 gridPosition;

	/// <summary>
	/// Processing performed by Unity when an instance is created.
	/// Control if a camera is associated, if it is not the case, it use the main camera.
	/// </summary>
	void Start ()
	{
		if (camAssociated == null)
			camAssociated = Camera.main;
		Update();
	}

	/// <summary>
	/// Called every frame, if the MonoBehaviour is enabled.
	/// Replace the grid if the associated camera has moved.
	/// </summary>
	void Update ()
	{

		if (gridPosition == this.transform.position)
			return;
		this.transform.position = new Vector3 (CalculDemiLePlusProche (camAssociated.transform.position.x), CalculDemiLePlusProche (camAssociated.transform.position.y), this.transform.position.z);
	}

	/// <summary>
	/// Calculate the number in .5 nearest to the value passed as argument.
	/// </summary>
	/// <returns>The nearest number in 0.5</returns>
	/// <param name="value">value</param>
	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
