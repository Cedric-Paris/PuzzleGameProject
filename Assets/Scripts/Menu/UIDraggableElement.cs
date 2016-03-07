using UnityEngine;
using System.Collections;

public abstract class UIDraggableElement : MonoBehaviour, Draggable
{
    private MenuButton _associatedMenuButton;

    // Use this for initialization
    void Start ()
    {
        this.transform.SetParent(GameObject.Find("MenuCanvas").transform);
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
        this.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
        Vector3 pos = this.GetComponent<RectTransform>().localPosition;
        pos.z = -1f;          // Empeche le collider de l'objet de se bloquer à cause du collider du bouton.
        this.GetComponent<RectTransform>().localPosition = pos;

        Debug.Log("Spawned object : " + this.GetComponent<RectTransform>().localPosition);

        _associatedMenuButton = GameObject.Find(GetAssociatedMenuButtonName()).GetComponent<MenuButton>();
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
