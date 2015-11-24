using UnityEngine;
using System.Collections;

public class DirectionProperties {

	public delegate Vector3 CalculFavoritePos(Vector3 currentPos);
	public CalculFavoritePos calculFavoritePos;

	public Vector3 direction;

	public Vector3 positionNextObserver;

	public int animationCode;

	public DirectionProperties (Vector3 direction, Vector3 positionNextObserver, int animationCode, CalculFavoritePos fonct)
	{
		this.direction = direction;
		this.positionNextObserver = positionNextObserver;
		this.animationCode = animationCode;
		this.calculFavoritePos = fonct;
	}
	
}
