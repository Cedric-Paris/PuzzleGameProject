using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public readonly static Vector2 GO_UP = new Vector2(0, 1);
    public readonly static Vector2 GO_DOWN = new Vector2(0, -1);
    public readonly static Vector2 GO_RIGHT = new Vector2(1, 0);
    public readonly static Vector2 GO_LEFT = new Vector2(-1, 0);

    private float speed = 0.005f;
    private Vector2 CurrentDirection = GO_LEFT;

    public SquareObserver currentSquare;
    public SquareObserver nextSquare;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!nextSquare.isTreated)
            TreatSquare(nextSquare);
        if (!currentSquare.isTreated)
            TreatSquare(currentSquare);
        transform.Translate(CurrentDirection * speed);
	}

    private void TreatSquare(SquareObserver square)
    {
        EffectTransformation eTransf = square.SquareDetected.Effect();
        square.isTreated = true;
        if (!eTransf.isChangingSomething)
            return;
        if (eTransf.newDirection != null)
            CurrentDirection = eTransf.newDirection;
        if (eTransf.newPosition != null)
            transform.position = eTransf.newPosition;
        ///////////////////////////////Traiter le cas des obstacles////////////////////////////////
    }
}
