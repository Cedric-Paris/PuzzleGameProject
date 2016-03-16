using UnityEngine;
using System.Collections;
using System.Security.Permissions;
using UnityEngine.UI;

/// <summary>
/// The MenuButton is an object that manages a specific element count.
/// </summary>
public abstract class MenuButton : MonoBehaviour
{
    /// <summary>
    /// The MenuButton's appearance.
    /// </summary>
    private Image _sprite;

    /// <summary>
    /// The <see cref="Text"/> where the element count is shown.
    /// </summary>
    private Text _elementCountText;


    /// <summary>
    /// The actual element count.
    /// </summary>
    public int ElementCount = 0;

    /// <summary>
    /// The <see cref="Sprite"/> to show when the element count is equal to 0.
    /// </summary>
    public Sprite OffSprite;

    /// <summary>
    /// The <see cref="Sprite"/> to show when the element count is greater than 0.
    /// </summary>
    public Sprite OnSprite;


    /// <summary>
    /// Initializes the MenuButton.
    /// </summary>
    void Start()
    {
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

    /// <summary>
    /// Called when this Object's Collider encounters another.
    /// </summary>
    /// <param name="other">The <see cref="Collider2D"/> that collided with the MenuButton.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        DraggableElement draggableElement = other.GetComponentInParent<DraggableElement>();
        if (draggableElement == null)
            return;

        if (!IsRightMovementAction(draggableElement))
            return;
        draggableElement.Die();
        UpElementCount();
    }

    /// <summary>
    /// Sets the appearance of the MenuButton to ON.
    /// </summary>
    private void SetOnSprite()
    {
        _sprite.sprite = OnSprite;
    }

    /// <summary>
    /// Sets the appearance of the MenuButton to OFF.
    /// </summary>
    private void SetOffSprite()
    {
        _sprite.sprite = OffSprite;
    }

    /// <summary>
    /// Sets the element count to the associated <see cref="Text"/>.
    /// </summary>
    private void SetElementCountToText()
    {
        _elementCountText.text = "x " + ElementCount.ToString();
    }

    /// <summary>
    /// Sets the element count to elementCount - 1.
    /// </summary>
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

    /// <summary>
    /// Sets the element count to elementCount + 1.
    /// </summary>
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

    /// <summary>
    /// Spawns the <see cref="UIDraggableElement"/> associated with this MenuButton on top of this MenuButton.
    /// </summary>
    private void SpawnUIDraggableElement()
    {
        Instantiate(GetUIElementToSpawn(), this.transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Informs whether the given <see cref="Element"/> is the <see cref="DraggableElement"/> bound the this MenuButton.
    /// </summary>
    /// <param name="draggableElement">The Element to test.</param>
    /// <returns>true = it is the right element || false = it is not the right element</returns>
    protected abstract bool IsRightMovementAction(DraggableElement draggableElement);

    /// <summary>
    /// Informs which <see cref="Text"/> is associated with this MenuButton.
    /// </summary>
    /// <returns>The name of the Text associated with this MenuButton.</returns>
    protected abstract string GetTextName();

    /// <summary>
    /// Informs which <see cref="UIDraggableElement"/> this MenuButton has to spawn.
    /// </summary>
    /// <returns>The <see cref="UIDraggableElement"/> bound to this MenuButton.</returns>
    protected abstract UIDraggableElement GetUIElementToSpawn();
}
