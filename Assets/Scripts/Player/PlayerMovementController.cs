using UnityEngine;
using System.Collections;

/// <summary>
/// Manages all Player movements
/// </summary>
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
	/// <summary>
	/// ElementObserver observing all non-obstacle elements
	/// </summary>
	public ElementObserver currentElement;
	/// <summary>
	/// ElementObserver observing all obstacle elements
	/// </summary>
	public ElementObserver obstacleElement;

	/// <summary>
	/// Player associated to this PlayerMovementController.
	/// </summary>
	public Player playerAssociated;

	/// <summary>
	/// Player speed.
	/// </summary>
	private float speed = 0.05f;
	
	public DirectionProperties CurrentDirection
	{
		get
		{
			if(currentDirection == null)
				return GO_RIGHT;//Direction by default
			return currentDirection;
		}
		set
		{
			currentDirection = value;
			OnPlayerDirectionChanging(value);
		}
	}
	private DirectionProperties currentDirection;
	public delegate void DirectionChange(DirectionProperties dir);
	public event DirectionChange onPlayerDirectionChanging;

	private bool isJumping;
	private Vector3 posEndJump;

	/// <summary>
	/// Called every frame, if the MonoBehaviour is enabled.
	/// Apply the effects of the encountered elements and moves the player.
	/// </summary>
	void Update ()
	{
		if(isJumping)
			CheckJumpState();
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
		if (isJumping || !eTransf.isChangingSomething)
			return;
		TreatmentIfObstacle(eTransf);//Evite Un cas de bug ou on passerait sur un obstacle
		if (eTransf.isWinner)
		{
			OnPlayerWin ();
			return;
		}
		if(eTransf.isStartingJump)
		{
			OnPlayerJump();
		}
		if (eTransf.isWater && playerAssociated!=null)
		{
			playerAssociated.FallInWater();
		}
		if (eTransf.newDirection != null)
		{
			transform.position = CurrentDirection.calculFavoritePos(transform.position);
			CurrentDirection = eTransf.newDirection;
		}
		if (eTransf.newPosition != new Vector3())
		{
			transform.position = CurrentDirection.calculFavoritePos(eTransf.newPosition);
		}
	}

	private void TreatObstacleElement(ElementObserver elementObs)
	{
		elementObs.isTreated = true;
		EffectTransformation eTransf = elementObs.ElementDetected.Effect();
		if(isJumping && !eTransf.isTall)
		{
			return;
		}
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
		ProgressionSave.LevelFinish(3);
		SceneLevelManager.main.LoadNextScene();
		Destroy(this.gameObject);
	}

	private void OnPlayerJump()
	{
		this.isJumping = true;
		Vector3 currentPosition = CurrentDirection.calculFavoritePos(this.transform.position);
		currentPosition.x += CurrentDirection.direction.x * 2;
		currentPosition.y += CurrentDirection.direction.y * 2;
		posEndJump = currentPosition;

		playerAssociated.OnPlayerJump();

	}

	private void CheckJumpState()
	{
		bool end = false;
		if(CurrentDirection == GO_UP && (this.transform.position.y >= posEndJump.y))
			end = true;
		if(CurrentDirection == GO_DOWN && (this.transform.position.y <= posEndJump.y))
			end = true;
		if(CurrentDirection == GO_RIGHT && (this.transform.position.x >= posEndJump.x))
			end = true;
		if(CurrentDirection == GO_LEFT && (this.transform.position.x <= posEndJump.x))
			end = true;
		if(end)
			OnPlayerEndJump();
	}

	private void OnPlayerEndJump()
	{
		this.isJumping = false;
		playerAssociated.OnPlayerEndJump();
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="dir">New direction.</param>
	private void OnPlayerDirectionChanging(DirectionProperties dir)
	{
		if (onPlayerDirectionChanging != null)
			onPlayerDirectionChanging (dir);
		BoxCollider2D bc = obstacleElement.GetComponent<BoxCollider2D>();
		if (bc != null)
			bc.size = dir.posObstacleObserver;
	}

	private float CalculDemiLePlusProche(float value)
	{
		if (value < 0)
			return ((int)value) - 0.5f;
		return ((int)value) + 0.5f;
	}
}
