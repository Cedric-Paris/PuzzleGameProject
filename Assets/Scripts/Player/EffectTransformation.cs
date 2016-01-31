using UnityEngine;
using System.Collections;

/// <summary>
/// Class representing the changes applied to the player by an element.
/// This class is used by the PlayerMovementController to know the effects of elements that the player come  across.
/// </summary>
public class EffectTransformation {
	
	/// <summary>
	/// Indicate whether there is an effect.
	/// false = The effect is not treatedé (no effect)
	/// </summary>
	public bool isChangingSomething;

	/// <summary>
	/// A new direction.
	/// </summary>
	public DirectionProperties newDirection;

	/// <summary>
	/// A new position.
	/// </summary>
    public Vector3 newPosition;

	/// <summary>
	/// Indicate whether the element encountered is an obstacle.
	/// </summary>
    public bool isObstacle;

	/// <summary>
	/// Indicate whether the element encountered is water.
	/// </summary>
	public bool isWater;

	/// <summary>
	/// Indicate whether the element encountered is an objective.
	/// </summary>
	public bool isObjectif;

	/// <summary>
	/// Indicate whether the element encountered is energy.
	/// </summary>
	public bool isEnergy;

	/// <summary>
	/// Specifies whether the player has won.
	/// </summary>
	public bool isWinner;

	/// <summary>
	/// Indicates whether the element jumped the player.
	/// </summary>
	public bool isStartingJump;

	/// <summary>
	/// Initializes a new instance of the <see cref="EffectTransformation"/> class.
	/// </summary>
	/// <param name="changingSomething">see <see cref="isChangingSomething"/></param>
	public EffectTransformation(bool changingSomething=true)
	{
		isChangingSomething = changingSomething;
	}

}
