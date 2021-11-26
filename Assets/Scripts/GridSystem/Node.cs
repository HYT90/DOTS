using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int x { get; private set; }
    public int y { get; private set; }

    public Vector3 worldPos;

    public Node(int _x, int _y, Vector3 _worldPos)
    {
        x = _x;
        y = _y;
        worldPos = _worldPos;
    }
}
