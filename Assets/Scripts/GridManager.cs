using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] 
    Vector2Int gridSize;
    [SerializeField]
    int unitGridSize;
    
    public int unityGridSize {get {return unitGridSize;}}
    
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    Dictionary<Vector2Int, Node> Grid {get {return grid;}}

    private void Awake()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coord = new Vector2Int(i, j);
                Node node = new Node(coord);
                grid.Add(coord, node);
                
                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //Vector3 position = new Vector3(coord.x * unitGridSize, 0, coord.y * unitGridSize);
                //sphere.transform.position = position;
                //sphere.transform.SetParent(transform);
            }
        }
    }
}
