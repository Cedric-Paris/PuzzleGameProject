using UnityEngine;

public class KillingObject : MonoBehaviour, IObjectWithEffectOnHit
{
    [SerializeField]
    private Square attachedTo;

    void Start()
    {
        if (attachedTo != null)
            attachedTo.Content = this;
    }

    public void ApplyEffect(Player player)
    {
        player.Kill();
    }
}
