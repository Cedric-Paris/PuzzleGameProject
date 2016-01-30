using UnityEngine;
using System.Collections;

public class DrawGrid : MonoBehaviour {

	public Camera camera;
	public GameObject gridPart;

	/// <summary>
	/// Processing performed by Unity when an instance is created
	/// </summary>
	void Start ()
	{
		float xMin, xMax, yMin, yMax;
		Vector3 pointBasGauche = camera.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		Vector3 pointBasDroite = camera.ScreenToWorldPoint (new Vector3 (Screen.width, 0, 0));
		Vector3 pointHautGauche = camera.ScreenToWorldPoint (new Vector3 (0, Screen.height, 0));
		xMin = CalculDemiLePlusProche (pointBasGauche.x);
		xMax = CalculDemiLePlusProche (pointBasDroite.x);
		yMin = CalculDemiLePlusProche (pointBasGauche.y);
		yMax = CalculDemiLePlusProche (pointHautGauche.y);

		for (float i = yMin; i<=yMax; i++)
		{
			for (float j = xMin; j<=xMax; j++)
			{
				((GameObject) Instantiate (gridPart, new Vector3(j,i, this.transform.position.z), Quaternion.identity)).transform.SetParent(this.gameObject.transform);
			}
		}
	}

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
