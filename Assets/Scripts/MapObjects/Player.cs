using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementController movementController;
    public PlayerMovementController MovementController { get { return movementController; }}

	// Use this for initialization
	void Start () {
        MovementController.NewSquareReached += OnNewSquareReached;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnNewSquareReached(PlayerMovementController sender, int x, int y)
    {
        Square currentSquare = TileMap.MainMap.GetSquare(x, y);
        if (currentSquare == null)
        {
            OnPlayerLeavesMap();
            return;
        }
        var objWithEffect = currentSquare.Content as IObjectWithEffectAtEntrance;
        if(objWithEffect != null)
        {
            objWithEffect.ApplyEffect(this);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        var obj = col.gameObject.GetComponent<IObjectWithEffectOnHit>();
        if(obj != null)
        {
            obj.ApplyEffect(this);
            Debug.Log("TOUCH");
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnPlayerLeavesMap()
    {
        Destroy(gameObject);
    }
}
