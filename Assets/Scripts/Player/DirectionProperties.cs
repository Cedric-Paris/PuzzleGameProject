using UnityEngine;
using System.Collections;

/// <summary>
/// Class that represent Player properties associated with a direction.
/// </summary>
public class DirectionProperties {

	/// <summary>
	/// Delegate type used to calculate the favorite position. (see <see cref="calculFavoritePos"/> attribute)
	/// </summary>
	public delegate Vector3 CalculFavoritePos(Vector3 currentPos);
	/// <summary>
	/// Delegate function used to calculate the position at which the player is repositioned when taking this <see cref="direction"/>.
	/// </summary>
	public CalculFavoritePos calculFavoritePos;

	/// <summary>
	/// Delegate type used for the treatment of squares. (see <see cref="squareCanBeTreat"/> attribute)
	/// </summary>
	public delegate bool SquareCanBeTreat(Vector3 currentPos);
	/// <summary>
	/// Delegate function used to determine if an item should be treated, to prevent it from being considered too early.
	/// </summary>
	public SquareCanBeTreat squareCanBeTreat;

	/// <summary>
	/// Position/Size of the obstacle observer collider for this <see cref="direction"/>.
	/// </summary>
	public Vector2 posObstacleObserver;

	/// <summary>
	/// Player direction associated with all these properties.
	/// </summary>
	public Vector3 direction;


	public Vector3 positionNextObserver;// A SUPPRIMER CAR PAS UTILISEE ACTUELLEMENT

	/// <summary>
	/// Player animation number for this <see cref="direction"/>.
	/// </summary>
	public int animationCode;

	/// <summary>
	/// Initializes a new instance of the <see cref="DirectionProperties"/> class.
	/// </summary>
	/// <param name="direction">Direction</param>
	/// <param name="posObstacleObserver">Position/Size of the obstacle observer.</param>
	/// <param name="positionNextObserver">Position next observer.</param>
	/// <param name="animationCode">Animation code.</param>
	/// <param name="fonctCal">Calculating function for the favorite position (see <see cref="calculFavoritePos"/> attribute)</param>
	/// <param name="fonctTreat">Function that determine when the treatment of an item need to be done (see <see cref="squareCanBeTreat"/> attribute).</param>
	public DirectionProperties (Vector3 direction,Vector2 posObstacleObserver, Vector3 positionNextObserver, int animationCode, CalculFavoritePos fonctCal, SquareCanBeTreat fonctTreat)
	{
		this.direction = direction;
		this.posObstacleObserver = posObstacleObserver;
		this.positionNextObserver = positionNextObserver;
		this.animationCode = animationCode;
		this.calculFavoritePos = fonctCal;
		this.squareCanBeTreat = fonctTreat;
	}
	
}
