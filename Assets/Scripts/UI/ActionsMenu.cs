using UnityEngine;

public class ActionsMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform PanelTransform;
    private ActionInMenu selectedAction;
    [SerializeField]
    public BoxCollider actionsCollider;

    [SerializeField]
    private int[] actionsCounter = new int[5];

    void Start()
    {
        GameManager.CameraSizeChanged += SetColliderSize;
        SetColliderSize();
    }

    public void ToggleSelect(ActionInMenu actionToSelect = null)
    {
        if (selectedAction != null)
            selectedAction.IsAnimated = false;
        if (actionToSelect != null && selectedAction != actionToSelect)
            actionToSelect.IsAnimated = true;
        selectedAction = actionToSelect;
    }

    private void SetColliderSize()
    {
        float panelWidth = PanelTransform.rect.width;
        actionsCollider.size = new Vector3(panelWidth, Screen.height, GameManager.GameUnitSizeInPixel * 2);
        actionsCollider.center = new Vector3((Screen.width - actionsCollider.size.x) / 2, 0, actionsCollider.size.z / -2);
    }

    void OnCollisionEnter(Collision collision)
    {
        var action = collision.gameObject.GetComponent<Action>();
        if (action == null)
            return;
        Debug.Log(action.MovementType);
        actionsCounter[(int)action.MovementType]++;
        Destroy(action.gameObject);
    }

    void OnDestroy()
    {
        GameManager.CameraSizeChanged -= SetColliderSize;
    }
}
