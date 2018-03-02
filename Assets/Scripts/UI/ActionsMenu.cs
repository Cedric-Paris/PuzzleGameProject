using UnityEngine;
using Assets.Scripts.Utilities;

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

    void Update()
    {
        if(selectedAction != null)
        {
            foreach(var touchInfo in  TouchInputManager.Touches)
            {
                Vector3 touchPos = GameManager.MainCamera.ScreenToWorldPoint(touchInfo.Touch.position);
                float t = (-touchPos.y) / GameManager.MainCamera.transform.forward.y;
                float interX = touchPos.x + (GameManager.MainCamera.transform.forward.x * t);
                float interZ = touchPos.z + (GameManager.MainCamera.transform.forward.z * t);

                Square square = TileMap.MainMap.GetSquare(interX, interZ);
                if (square != null && !square.HasContent && selectedAction.InstantiateAction(square))
                {
                    actionsCounter[selectedAction.ActionIndex]--;
                    if(actionsCounter[selectedAction.ActionIndex]<=0)
                    {
                        ToggleSelect(selectedAction);
                    }
                    TouchInputManager.Handled(touchInfo);
                    break;
                }
            }
        }
    }

    public void ToggleSelect(ActionInMenu actionToSelect = null)
    {
        if (selectedAction != null)
            selectedAction.IsAnimated = false;
        if (actionToSelect != null)
        {
            if (selectedAction == actionToSelect || actionsCounter[actionToSelect.ActionIndex] <= 0)
                actionToSelect = null;
            else
                actionToSelect.IsAnimated = true;
        }
        selectedAction = actionToSelect;
    }

    private void SetColliderSize()
    {
        float panelWidth = PanelTransform.rect.width;
        actionsCollider.size = new Vector3(panelWidth, Screen.height, GameManager.GameUnitSizeInPixel * 2);
        actionsCollider.center = new Vector3((Screen.width - actionsCollider.size.x) / 2, 0, actionsCollider.size.z / -2);
    }

    void OnTriggerEnter(Collider collider)
    {
        var action = collider.gameObject.GetComponent<Action>();
        if (action == null)
            return;
        actionsCounter[(int)action.MovementType]++;
        Destroy(action.gameObject);
    }

    void OnDestroy()
    {
        GameManager.CameraSizeChanged -= SetColliderSize;
    }
}
