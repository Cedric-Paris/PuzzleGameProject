using UnityEngine;

public class KillingObject : MonoBehaviour, IObjectWithEffect
{

    public void ApplyEffect(Player player)
    {
        player.Kill();
    }
}
