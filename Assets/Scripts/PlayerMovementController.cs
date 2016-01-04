using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
	
	public static readonly DirectionProperties GO_UP = new DirectionProperties (new Vector3 (0, 1, 0), new Vector2(0.23f, 0.43f), new Vector3 (0, 0.5f, 0), 3,
	                                                                            (currentPos) => { 
																									if(currentPos.y < 0) return new Vector3 (currentPos.x, ((int)currentPos.y) - 0.5f, 0);
																									return new Vector3 (currentPos.x, ((int)currentPos.y) + 0.5f, 0);},
																				(currentPos) => {
																									if (currentPos.y < 0 && (currentPos.y * -1) % 1 <= 0.5f) return true;
																									if (currentPos.y > 0 && (currentPos.y % 1) >= 0.5f) return true;
																									return false;
																								});
	public static readonly DirectionProperties GO_DOWN = new DirectionProperties(new Vector3(0, -1, 0), new Vector2(0.23f, 0.43f), new Vector3 (0, -0.5f, 0), 4,
	                                                                            (currentPos) => {
																									if(currentPos.y < 0) return new Vector3 (currentPos.x, ((int)currentPos.y) - 0.5f, 0);
																									return new Vector3 (currentPos.x, ((int)currentPos.y) + 0.5f, 0);},
																				(currentPos) => {
																									if (currentPos.y < 0 && (currentPos.y * -1) % 1 >= 0.5f) return true;
																									if (currentPos.y > 0 && (currentPos.y % 1) <= 0.5f) return true;
																									return false;
																								});
	public static readonly DirectionProperties GO_RIGHT = new DirectionProperties(new Vector3(1, 0, 0), new Vector2(0.43f, 0.23f), new Vector3 (0.5f, 0, 0), 1,
	                                                                              	(currentPos) => {
																										if(currentPos.x < 0) return new Vector3(((int)currentPos.x)-0.5f, currentPos.y,0);
																										return new Vector3(((int)currentPos.x)+0.5f, currentPos.y,0);},
																					(currentPos) => {
																										if (currentPos.x < 0 && (currentPos.x * -1) % 1 <= 0.5f) return true;
																										if (currentPos.x > 0 && (currentPos.x % 1) >= 0.5f) return true;
																										return false;
																									});
	public static readonly DirectionProperties GO_LEFT = new DirectionProperties(new Vector3(-1, 0, 0), new Vector2(0.43f, 0.23f), new Vector3 (-0.5f, 0, 0), 2,
                                                                            	(currentPos) => {
																									if(currentPos.x < 0) return new Vector3(((int)currentPos.x)-0.5f, currentPos.y,0);
																									return new Vector3(((int)currentPos.x)+0.5f, currentPos.y,0);},
																				(currentPos) => {
																									if (currentPos.x < 0 && (currentPos.x * -1) % 1 >= 0.5f) return true;
																									if (currentPos.x > 0 && (currentPos.x % 1) <= 0.5f) return true;
																									return false;
																								});

	public ElementObserver currentElement;
	public ElementObserver obstacleElement;

	public Player playerAssociated;

	private float speed = 0.05f;

	public DirectionProperties CurrentDirection
	{
		get
		{
			return currentDirection;
		}
		set
		{
			currentDirection = value;
			OnPlayerDirectionChanging(value);
		}
	}
	private DirectionProperties currentDirection = GO_RIGHT;
	public delegate void DirectionChange(DirectionProperties dir);
	public event DirectionChange onPlayerDirectionChanging;


	void Update () {
		if (! obstacleElement.isTreated)
			TreatObstacleElement(obstacleElement);
		if ((!currentElement.isTreated) && CurrentDirection.squareCanBeTreat(transform.position))
			TreatElement(currentElement);
		transform.Translate(CurrentDirection.direction * speed);
	}

	private void TreatElement(ElementObserver elementObs)
	{
		elementObs.isTreated = true;
		EffectTransformation eTransf = elementObs.ElementDetected.Effect(true);
		if (! eTransf.isChangingSomething)
			return;
		TreatmentIfObstacle (eTransf);//Evite Un cas de bug ou on passerait sur un obstacle
		if (eTransf.isWinner)
		{
			OnPlayerWin ();
			return;
		}
		if (eTransf.newDirection != null)
		{
			transform.position = CurrentDirection.calculFavoritePos(transform.position);
			CurrentDirection = eTransf.newDirection;
			/*nextSquare.transform.position = transform.position + eTransf.newDirection.positionNextObserver;
			 	A supprimer si plus utilisé*/
		}
	}

	private void TreatObstacleElement(ElementObserver elementObs)
	{
		elementObs.isTreated = true;
		EffectTransformation eTransf = elementObs.ElementDetected.Effect();
		TreatmentIfObstacle (eTransf);
	}

	private void TreatmentIfObstacle(EffectTransformation eTransf)
	{
		if (eTransf.isObstacle && playerAssociated!=null)
			playerAssociated.Explode();
	}

	private void OnPlayerWin()
	{
		this.speed = 0;
	}

	private void OnPlayerDirectionChanging(DirectionProperties dir)
	{
		if (onPlayerDirectionChanging != null)
			onPlayerDirectionChanging (dir);
		BoxCollider2D bc = obstacleElement.GetComponent<BoxCollider2D>();
		if (bc != null)
			bc.size = dir.posObstacleObserver;
	}
}
