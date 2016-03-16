using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The UIDraggableElement is the element the <see cref="MenuButton"/> will spawn. This element is spawned so that there is always one and only one UIDraggableElement on top of the <see cref="MenuButton"/> when the button's count is greater than 0. This UIDraggableElement is <see cref="Draggable"/> so the player can move it around with its finger to the <see cref="Square"/> on the map he wants the corresponding <see cref="Element"/> to spawn.
/// </summary>
public abstract class UIDraggableElement : MonoBehaviour, Draggable
{
    /// <summary>
    /// The <see cref="MenuButton"/> associated with this UIDraggableElement, the one the UIDraggableElement is on top of.
    /// </summary>
    private MenuButton _associatedMenuButton;

    /// <summary>
    /// Initializes the UIDraggableElement.
    /// </summary>
    void Start ()
    {
        GameObject canvas = GameObject.Find("MenuCanvas");
        transform.SetParent(canvas.transform);
        _associatedMenuButton = GameObject.Find(GetAssociatedMenuButtonName()).GetComponent<MenuButton>();
        
        Resize(canvas);
        
        float size = Camera.main.WorldToScreenPoint(new Vector3(1, 0, 0)).x -
                     Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)).x;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size, size);
    }

    /// <summary>
    /// Resizes the UIDraggableElement so it matches the size of the correponding <see cref="MenuButton"/>.
    /// </summary>
    /// <param name="canvas">The menu <see cref="Canvas"/>.</param>
    private void Resize(GameObject canvas)
    {
        RectTransform menuRectTransform = canvas.GetComponentInChildren<CanvasGroup>().GetComponent<RectTransform>();
        RectTransform menuButtonRectTransform = _associatedMenuButton.GetComponent<RectTransform>();

        RectTransform rt = GetComponent<RectTransform>();

        Vector2 minMenu = menuRectTransform.anchorMin;
        Vector2 maxMenu = menuRectTransform.anchorMax;
        rt.anchorMin = new Vector2(minMenu.x + menuButtonRectTransform.anchorMin.x * (maxMenu.x - minMenu.x),
            minMenu.y + menuButtonRectTransform.anchorMin.y * (maxMenu.y - minMenu.y));
        rt.anchorMax = new Vector2(maxMenu.x - menuButtonRectTransform.anchorMax.x * (maxMenu.x - minMenu.x),
            menuRectTransform.anchorMax.y - (1 - menuButtonRectTransform.anchorMax.y) * (maxMenu.y - minMenu.y));
        rt.localScale = new Vector3(1, 1, 1);
        rt.offsetMin = new Vector2(0, 0);
        rt.offsetMax = new Vector2(0, 0);

        Vector3 pos = rt.localPosition;
        pos.z = -1f;          // Empeche le collider de l'objet de se bloquer à cause du collider du bouton.
        rt.localPosition = pos;
    }

    /// <summary>
    /// Implemented from the <see cref="Draggable"/> interface.
    /// If the object has a <see cref="Collider"/>, a touch or a click will drag the object. Here, we make the UIDraggableElement follow the mouse's cursor.
    /// </summary>
    public void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
    }

    /// <summary>
    /// Implemented from the <see cref="Draggable"/> interface.
    /// If the object has a <see cref="Collider"/>, a touch or a click will drag the object. This method is called when the object is dropped by the mouse or finger. Here, we move the UIDraggableElement to its last position.
    /// </summary>
    public void OnMouseUp()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
        OnMouseDrop();
    }

    /// <summary>
    /// Implemented from the <see cref="Draggable"/> interface.
    /// If the object has a <see cref="Collider"/>, a touch or a click will drag the object. This method is called when the object is dropped by the mouse or finger. Here, we spawn the <see cref="Element"/> associated with this UIDraggableElement at the right position and destroy this object.
    /// </summary>
    public void OnMouseDrop()
    {
        Vector3 favoritePosition = new Vector3(0, 0, 0);
        favoritePosition.x = CalculDemiLePlusProche(this.transform.position.x);
        favoritePosition.y = CalculDemiLePlusProche(this.transform.position.y);

        Instantiate(GetElementBase(), favoritePosition, Quaternion.identity);

        Destroy(this.gameObject);
    }

    /// <summary>
    /// Implemented from the <see cref="Draggable"/> interface.
    /// If the object has a <see cref="Collider"/>, a touch or a click will drag the object. This method is called when the object is picked up by the mouse or finger. Here, we tell the associated <see cref="MenuButton"/> this UIDraggableElement was taken.
    /// </summary>
    public void OnMouseDown()
    {
        _associatedMenuButton.DownElementCount();
    }

    /// <summary>
    /// Calculates the nearest n.5 float.
    /// </summary>
    /// <param name="value">The value for which we want to get the nearest n.5 float.</param>
    /// <returns>The nearest n.5 float.</returns>
    private float CalculDemiLePlusProche(float value)
    {
        if (value < 0)
            return ((int)value) - 0.5f;
        return ((int)value) + 0.5f;
    }

    /// <summary>
    /// Informs which Element this UIDraggableElement has to spawn.
    /// </summary>
    /// <returns>The Base Element this UIDraggableElement has to spawn</returns>
    protected abstract DraggableElement GetElementBase();

    /// <summary>
    /// Informs which button is associated with this UIDraggableElement.
    /// </summary>
    /// <returns>The name of the button associated with this UIDraggableElement.</returns>
    protected abstract string GetAssociatedMenuButtonName();
}
