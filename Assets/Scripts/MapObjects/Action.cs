using UnityEngine;

public class Action : MonoBehaviour, IObjectWithEffectAtEntrance
{
    [SerializeField]
    private bool isDestructible;

    [SerializeField]
    [Range(0, 19)]
    private int destroyPhaseNumber;

    [SerializeField]
    private Square attachedTo;

    [SerializeField]//Component needed to display the field in Unity Inspector
    private Component movementEffect;

    public IMovementProcedure MovementEffect { get { return movementEffect as IMovementProcedure; } }

    void Start()
    {
        if (attachedTo != null)
            attachedTo.Content = this;
    }

    public void ApplyEffect(Player player)
    {
        if (MovementEffect != null)
            player.MovementController.AddMovementProcedure(MovementEffect);
        if(isDestructible)
        {
            player.MovementController.PhasePointReached += OnPhasePointReached;
        }
    }

    private void OnPhasePointReached(PlayerMovementController sender, int phaseNumber)
    {
        Debug.Log("Called");
        if (phaseNumber >= destroyPhaseNumber)
        {
            sender.PhasePointReached -= OnPhasePointReached;
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        if(attachedTo != null)
            attachedTo.Content = null;
    }

}
