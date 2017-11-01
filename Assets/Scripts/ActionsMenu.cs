using UnityEngine;

public class ActionsMenu : MonoBehaviour
{
    [SerializeField]
    private int[] actionsCounter = new int[5];

    void OnCollisionEnter(Collision collision)
    {
        var action = collision.gameObject.GetComponent<Action>();
        if (action == null)
            return;
        Debug.Log(action.MovementType);
        actionsCounter[(int)action.MovementType]++;
        Destroy(action.gameObject);
    }
}
