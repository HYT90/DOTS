using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public readonly int width;
    public readonly int height;
    private readonly float cellRadius;
    private Vector3 offset;


    public Node[,] gridNode { get; private set; }

    public Grid(int _width, int _height, float _cellRadius, Vector3 _offset)
    {
        width = _width;
        height = _height;
        cellRadius = _cellRadius;
        offset = _offset;

        gridNode = new Node[width, height];

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                Vector3 l = offset + new Vector3(j * cellRadius * 2, 0, i * cellRadius * 2);
                gridNode[j, i] = new Node(j, i, l);
                Debug.DrawLine(l, l + Vector3.right * cellRadius * 2, Color.green, 100f);
                Debug.DrawLine(l, l + Vector3.forward * cellRadius * 2, Color.green, 100f);
            }
        }
    }

    public Vector2Int GetNode(Vector3 mousePos)
    {
        Vector3 dis = mousePos - offset;
        float x = dis.x / (cellRadius * 2);
        float y = dis.z / (cellRadius * 2);

        if(x > width-1 || x < 0)
        {
            x = x < 0 ? 0 : width-1;
        }
        if(y > height-1 || y < 0)
        {
            y = y < 0 ? 0 : height-1;
        }

        return new Vector2Int((int)x, (int)y);
    }
}
