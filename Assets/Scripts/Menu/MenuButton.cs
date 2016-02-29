using UnityEngine;
using System.Collections;
using System.Security.Permissions;
using UnityEngine.UI;

public abstract class MenuButton : MonoBehaviour
{
    private Image _sprite;
    private Text _elementCountText;

    public int ElementCount = 0;
    public Sprite OffSprite;
    public Sprite OnSprite;


    // Use this for initialization
    void Start()
    {
        Debug.Log("Start de la maman :)");

        float size = Camera.main.WorldToScreenPoint(new Vector3(1, 0, 0)).x -
                     Camera.main.WorldToScreenPoint(new Vector3(0, 0, 0)).x;
        this.GetComponent<BoxCollider2D>().size = new Vector2(size, size);
        
        _elementCountText = GameObject.Find(GetTextName()).GetComponent<Text>();

        _sprite = this.GetComponent<Image>();
        if (ElementCount == 0)
        {
            SetOffSprite();
            return;
        }

        SetOnSprite();
        SpawnUIDraggableElement();

    }

    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("OnTriggerEnter2D on " + this.name);

        DraggableElement draggableElement = other.GetComponentInParent<DraggableElement>();
        if (draggableElement == null)
            return;

            Debug.Log("OtherGameObject = " + draggableElement.gameObject.name);

        if (!IsRightMovementAction(draggableElement))
            return;
        draggableElement.Die();
        UpElementCount();
    }

    private void SetOnSprite()
    {
        _sprite.sprite = OnSprite;
    }

    private void SetOffSprite()
    {
        _sprite.sprite = OffSprite;
    }

    private void SetElementCountToText()
    {
        _elementCountText.text = "x " + ElementCount.ToString();
    }

    public void DownElementCount()
    {
        -- ElementCount;
        SetElementCountToText();
        if (ElementCount == 0)
        {
            SetOffSprite();
            return;
        }
        SpawnUIDraggableElement();
    }

    public void UpElementCount()
    {
        if (ElementCount == 0)
        {
            SetOnSprite();
            SpawnUIDraggableElement();
        }
        ++ElementCount;
        SetElementCountToText();
    }

    private void SpawnUIDraggableElement()
    {
        Debug.Log("Spawn est appelé !!!");
        Instantiate(GetUIElementToSpawn(), this.transform.position, Quaternion.identity);
    }

    protected abstract bool IsRightMovementAction(DraggableElement draggableElement);

    protected abstract string GetTextName();

    protected abstract UIDraggableElement GetUIElementToSpawn();
}
