using UnityEngine;

public class KillingObject : MonoBehaviour, IObjectWithEffectOnHit
{

    public void ApplyEffect(Player player)
    {
        player.Kill();
    }
}
