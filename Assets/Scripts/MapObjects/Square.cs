using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{

    public Component Content { get; set; }

    void Start()
    {
        TileMap.MainMap.Tiles[(int)transform.position.x, (int)transform.position.z] = this;
    }
}