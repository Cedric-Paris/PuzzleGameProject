public interface IObjectWithEffect
{
    void ApplyEffect(Player player);
}

//AT Square Entrance
public interface IObjectWithEffectAtEntrance : IObjectWithEffect {}

//On hit
public interface IObjectWithEffectOnHit : IObjectWithEffect { }