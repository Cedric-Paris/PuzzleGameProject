using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The UIDraggableElement is the element the <see cref="MenuButton"/> will spawn. This element 
/// </summary>
public abstract class UIDraggableElement : MonoBehaviour, Draggable
{
    private MenuButton _associatedMenuButton;

    // Use this for initialization
    void Start ()
    {
        GameObject canvas = GameObject.Find("MenuCanvas");
        transform.SetParent(canvas.transform);
        _associatedMenuButton = GameObject.Find(GetAssociatedMenuButtonName()).GetComponent<MenuButton>();
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
        
        float size = Camera.main.WorldToScreenPoint(new Vector3(1, 0, 0)).x -
                     Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)).x;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size, size);
        

    }
    
    //La methode OnMouseDrag necessite un collider sur l'element a bouger 
    public void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
    }

    public void OnMouseUp()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePos.z = 0;
        this.transform.position = mousePos;
        OnMouseDrop();

    }

    public void OnMouseDrop()
    {
        Debug.Log(this.name + " : OnMouseDrop");

        // Quand OnDrop, spawn le bon élément au bon endroit

        Vector3 favoritePosition = new Vector3(0, 0, 0);
        favoritePosition.x = CalculDemiLePlusProche(this.transform.position.x);
        favoritePosition.y = CalculDemiLePlusProche(this.transform.position.y);

        Instantiate(GetElementBase(), favoritePosition, Quaternion.identity);

        Destroy(this.gameObject);
    }

    public void OnMouseDown()
    {
        _associatedMenuButton.DownElementCount();
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        this.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        Debug.Log(this.name + " : OnMouseDown");
    }

    private float CalculDemiLePlusProche(float value)
    {
        if (value < 0)
            return ((int)value) - 0.5f;
        return ((int)value) + 0.5f;
    }

    protected abstract DraggableElement GetElementBase();

    protected abstract string GetAssociatedMenuButtonName();
}
