using UnityEngine;

public class Square : MonoBehaviour
{
    public virtual bool HasContent { get { return Content != null; } }

    public Component Content { get; set; }

    protected void Start()
    {
        TileMap.MainMap.Tiles[(int)transform.position.x, (int)transform.position.z] = this;
    }
}