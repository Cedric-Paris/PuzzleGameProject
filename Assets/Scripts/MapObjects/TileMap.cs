using UnityEngine;

public class TileMap : MonoBehaviour
{
    public static TileMap MainMap;

    public const int X_MAX = 100;
    public const int Y_MAX = 100;

    public Square[,] Tiles = new Square[X_MAX, Y_MAX];

    public int XLimit { get; set; }
    public int YLimit { get; set; }

    void Awake()
    {
        MainMap = this;
    }

    public Square GetSquare(int x, int y)
    {
        if(X_MAX <= x || Y_MAX <= y || x < 0 || y < 0)
            return null;
        return Tiles[x, y];
    }

    public Square GetSquare(float x, float y)
    {
        return GetSquare(Mathf.FloorToInt(x), Mathf.FloorToInt(y));
    }
}
