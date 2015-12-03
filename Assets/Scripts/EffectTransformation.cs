using UnityEngine;
using System.Collections;

public class EffectTransformation {
    /*
    
        Ajouter des attributs pour traiter des effets particulier
        Chaque attribut peut rester null si l'effet ne les change pas
    
    */
	public bool isChangingSomething;//Passer a true quand il y a un effet sinon il ne sera pas traité

	public DirectionProperties newDirection;

    public Vector3 newPosition;

    public bool isObstacle;

	public bool isObjectif;

	public bool isEnergy;
	
	public bool isWinner;

	public bool isStartingJump;

	public EffectTransformation(bool changingSomething=true)
	{
		isChangingSomething = changingSomething;
	}

}
