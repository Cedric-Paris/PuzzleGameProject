using UnityEngine;
using TouchInfo = TouchInputManager.TouchInfo;

public class Action : MonoBehaviour, IObjectWithEffectAtEntrance
{
    public enum MovementEffectType
    {
        GoNorth = 0,
        GoSouth = 1,
        GoEast = 2,
        GoWest = 3,
        Jump = 4
    }

    [SerializeField]
    private bool isDestructible;

    [SerializeField]
    [Range(0, 19)]
    private int destroyPhaseNumber;

    [SerializeField]
    private Square attachedTo;

    [SerializeField]
    private MovementEffectType movementType;
    public MovementEffectType MovementType { get { return movementType; } }


    [SerializeField]
    private Collider actionCollider;

    [SerializeField]
    private bool canBeDragged;
    public bool CanBeDragged
    {
        get { return canBeDragged; }
        set
        {
            canBeDragged = value;
            if (actionCollider == null) return;
            if (canBeDragged)
                actionCollider.enabled = true;
            else
                actionCollider.enabled = false;
        }
    }

    private Vector3 InitialPos;
    private int fingerDraggingId = -1;

    void Start()
    {
        if (attachedTo != null)
            attachedTo.Content = this;
        CanBeDragged = canBeDragged;
    }

    void Update()
    {
        if(CanBeDragged)
        {
            if(fingerDraggingId != -1)
            {
                TouchInfo touchInfo;
                if (TouchInputManager.TryGetTouch(fingerDraggingId, out touchInfo))
                {
                    transform.position = GameManager.mainCamera.ScreenToWorldPoint(touchInfo.Touch.position) + GameManager.mainCamera.transform.forward;
                }
                if (touchInfo == null || touchInfo.Touch.phase == TouchPhase.Ended || touchInfo.Touch.phase == TouchPhase.Canceled)
                    EndDrag();
            }
            else
            {
                var touches = TouchInputManager.Touches;
                foreach(TouchInfo t in touches)
                {
                    if(t.ObjectTouched == this.gameObject)
                    {
                        TouchInputManager.Handled(t);
                        InitialPos = transform.position;
                        transform.position = GameManager.mainCamera.ScreenToWorldPoint(t.Touch.position) + GameManager.mainCamera.transform.forward;
                        if (t.Touch.phase != TouchPhase.Ended && t.Touch.phase != TouchPhase.Canceled)
                            fingerDraggingId = t.Touch.fingerId;
                        else
                            EndDrag();
                        break;
                    }
                }
            }
            
        }
    }

    private void EndDrag()
    {
        fingerDraggingId = -1;

        //Project position point on the TileMap : 
        float t = (transform.position.y *-1) / GameManager.mainCamera.transform.forward.y;
        float xProjection = transform.position.x + (GameManager.mainCamera.transform.forward.x * t);
        float zProjection = transform.position.z + (GameManager.mainCamera.transform.forward.z * t);
        int squareIndexX = Mathf.FloorToInt(xProjection);
        int squareIndexY = Mathf.FloorToInt(zProjection);

        Square s = TileMap.MainMap.GetSquare(squareIndexX, squareIndexY);
        if(s == null || s.Content != null)
        {
            transform.position = InitialPos;
        }
        else
        {
            s.Content = this;
            attachedTo.Content = null;
            attachedTo = s;
            transform.position = new Vector3(squareIndexX+0.5f, 0.2f, squareIndexY+0.5f);
        }
    }

    public void ApplyEffect(Player player)
    {
        var movementEffect = GetIMovementProc(MovementType);
        if (movementEffect != null)
            player.MovementController.AddMovementProcedure(movementEffect);
        if(isDestructible)
        {
            player.MovementController.PhasePointReached += OnPhasePointReached;
        }
    }

    private void OnPhasePointReached(PlayerMovementController sender, int phaseNumber)
    {
        if (phaseNumber >= destroyPhaseNumber)
        {
            sender.PhasePointReached -= OnPhasePointReached;
            Destroy(this.gameObject);
        }
    }

    private IMovementProcedure GetIMovementProc(MovementEffectType movType)
    {
        switch(movType)
        {
            case MovementEffectType.GoNorth:
                return new TurnProcedure(Direction.North);
            case MovementEffectType.GoSouth:
                return new TurnProcedure(Direction.South);
            case MovementEffectType.GoEast:
                return new TurnProcedure(Direction.East);
            case MovementEffectType.GoWest:
                return new TurnProcedure(Direction.West);
            case MovementEffectType.Jump:
                return new JumpProcedure();
            default:
                return null;
        }
    }

    void OnDestroy()
    {
        if(attachedTo != null)
            attachedTo.Content = null;
        if (fingerDraggingId != -1)
            TouchInputManager.Unhandled(fingerDraggingId);
        fingerDraggingId = -1;
    }

}
