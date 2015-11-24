using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {

	public static readonly DirectionProperties GO_UP = new DirectionProperties (new Vector3 (0, 1, 0), new Vector3 (0, 0.5f, 0), -1,
	                                                                            delegate(Vector3 currentPos)
	                                                                            {
																					return new Vector3(currentPos.x, ((int)currentPos.y)+0.5f,0);
																				});
	public static readonly DirectionProperties GO_DOWN = new DirectionProperties(new Vector3(0, -1, 0), new Vector3 (0, -0.5f, 0), -1,
	                                                                             delegate(Vector3 currentPos)
	                                                                             {
		return new Vector3(currentPos.x, ((int)currentPos.y)+0.5f,0);
	});
	public static readonly DirectionProperties GO_RIGHT = new DirectionProperties(new Vector3(1, 0, 0), new Vector3 (0.5f, 0, 0), -1,
	                                                                              delegate(Vector3 currentPos)
	                                                                              {
		return new Vector3(((int)currentPos.x)+0.5f, currentPos.y,0);
	});
	public static readonly DirectionProperties GO_LEFT = new DirectionProperties(new Vector3(-1, 0, 0), new Vector3 (-0.5f, 0, 0), -1,
	                                                                             delegate(Vector3 currentPos)
	                                                                             {
		return new Vector3(((int)currentPos.x)+0.5f, currentPos.y,0);
																				 });

	public SquareObserver currentSquare;
	public SquareObserver nextSquare;

	private float speed = 0.05f;
	private DirectionProperties currentDirection = GO_RIGHT;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if ((!currentSquare.isTreated) && transform.position.x%1 >= 0.5f )
			TreatSquare(currentSquare);
		transform.Translate(currentDirection.direction * speed);
	}

	private void TreatSquare(SquareObserver square)
	{
		square.isTreated = true;
		EffectTransformation eTransf = square.ElementDetected.Effect();
		/*if (! eTransf.isChangingSomething)
			return;*/
		if (eTransf.newDirection != null)
		{
			transform.position = currentDirection.calculFavoritePos(transform.position);
			currentDirection = eTransf.newDirection;
			nextSquare.transform.position = transform.position + eTransf.newDirection.positionNextObserver;
		}
	}
}
