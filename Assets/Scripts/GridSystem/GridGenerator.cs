using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int x;
    public int y;
    public float radius;
    public Vector3 offset;
    Grid grid;

    public GameObject cube;

    private void Awake()
    {
        grid = new Grid(x, y, radius, offset);

        for(int i = 0; i < y; i++)
        {
            for(int j = 0; j < x; j++)
            {
                Instantiate(cube, grid.gridNode[j, i].worldPos, Quaternion.identity);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(!Physics.Raycast(ray, out RaycastHit hit, 100f)) return;
            Debug.Log(grid.GetNode(hit.point) + "..." + hit.point);
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
