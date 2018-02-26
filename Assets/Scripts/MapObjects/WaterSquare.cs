public class WaterSquare : Square
{
    public override bool HasContent { get { return true; } }

    protected new void Start()
    {
        base.Start();
    }
}
