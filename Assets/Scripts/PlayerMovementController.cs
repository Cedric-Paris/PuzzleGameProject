using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	DirectionProperties GO_UP = new DirectionProperties (new Vector3 (0, 1, 0), new Vector3 (0, 0.5f, 0), -1);
	DirectionProperties GO_DOWN = new DirectionProperties(new Vector3(0, -1, 0), new Vector3 (0, -0.5f, 0), -1);
	DirectionProperties GO_RIGHT = new DirectionProperties(new Vector3(1, 0, 0), new Vector3 (0.5f, 0, 0), -1);
	DirectionProperties GO_LEFT = new DirectionProperties(new Vector3(-1, 0, 0), new Vector3 (-0.5f, 0, 0), -1);

	public SquareObserver currentSquare;
	public SquareObserver nextSquare;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!nextSquare.isTreated)
			TreatSquare(nextSquare);
		if (!currentSquare.isTreated && transform.position.x%1 >= 0.5f )
			TreatSquare(currentSquare);
		transform.Translate(currentDirection * speed);
	}
}
