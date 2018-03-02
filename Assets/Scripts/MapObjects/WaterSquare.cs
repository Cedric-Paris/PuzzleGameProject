using UnityEngine;

public class WaterSquare : Square, IObjectWithEffectAtEntrance
{
    public override bool HasContent { get { return true; } }

    public override Component Content
    {
        get { return this; }
        set { }
    }

    protected new void Start()
    {
        base.Start();
    }

    public void ApplyEffect(Player player)
    {
        player.MovementController.AddMovementProcedure(new SinkProcedure(player));
    }
}
