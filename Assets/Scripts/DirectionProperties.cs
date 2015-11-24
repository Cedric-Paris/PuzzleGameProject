﻿using UnityEngine;
using System.Collections;

public class DirectionProperties {

	public delegate Vector3 CalculFavoritePos(Vector3 currentPos);
	public CalculFavoritePos calculFavoritePos;

	public delegate bool SquareCanBeTreat(Vector3 currentPos);
	public SquareCanBeTreat squareCanBeTreat;

	public Vector3 direction;

	public Vector3 positionNextObserver;

	public int animationCode;

	public DirectionProperties (Vector3 direction, Vector3 positionNextObserver, int animationCode, CalculFavoritePos fonctCal, SquareCanBeTreat fonctTreat)
	{
		this.direction = direction;
		this.positionNextObserver = positionNextObserver;
		this.animationCode = animationCode;
		this.calculFavoritePos = fonctCal;
		this.squareCanBeTreat = fonctTreat;
	}
	
}
