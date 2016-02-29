using UnityEngine;
using System.Collections;

public class DraggableElement : Element, Draggable {

    public Element elementBase;

    void Start()
    {
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

        Vector3 favoritePosition = new Vector3(0, 0, 0);
        favoritePosition.x = CalculDemiLePlusProche(this.transform.position.x);
        favoritePosition.y = CalculDemiLePlusProche(this.transform.position.y);

        this.transform.position = favoritePosition;
    }

    public void OnMouseDown()
    {
        Debug.Log(this.name + " : OnMouseDown");
    }

    private float CalculDemiLePlusProche(float value)
    {
        if (value < 0)
            return ((int)value) - 0.5f;
        return ((int)value) + 0.5f;
    }

    public override EffectTransformation Effect(bool isTreated = false)
    {
        return elementBase.Effect(isTreated);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
