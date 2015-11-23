using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public readonly static Vector2 GO_UP = new Vector2(0, 1);
    public readonly static Vector2 GO_DOWN = new Vector2(0, -1);
    public readonly static Vector2 GO_RIGHT = new Vector2(1, 0);
    public readonly static Vector2 GO_LEFT = new Vector2(-1, 0);

    private float speed = 0.005f;
    private Vector2 currentDirection = GO_RIGHT;

    public SquareObserver currentSquare;
    public SquareObserver nextSquare;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void TreatSquare(SquareObserver square)
    {
        EffectTransformation eTransf = square.ElementDetected.Effect();
        square.isTreated = true;
        if (!eTransf.isChangingSomething)
            return;
        if (eTransf.newDirection != null)
		{
			currentDirection = eTransf.newDirection;
			nextSquare.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
		}
        if (eTransf.newPosition == null)
            transform.position = eTransf.newPosition;
        ///////////////////////////////Traiter le cas des obstacles////////////////////////////////
    }
}
