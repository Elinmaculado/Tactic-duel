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
    public Dictionary<Vector2Int, Node> Grid {get {return grid;}}

    private void Awake()
    {
        CreateGrid();
    }
    
    public Node GetNode(Vector2Int coord)
    {
        if (Grid.ContainsKey(coord))
            return Grid[coord];
        else
            return null;
    }
    
    public void BlockNode(Vector2Int coord)
    {
        if (Grid.ContainsKey(coord))
            Grid[coord].walkable = false;
    }
    
    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid )
        {
            entry.Value.connectTo = null;
            entry.Value.explored = false;
            entry.Value.path = false;
            //entry.Value.walkable = true;
        }
    }
    
    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coord)
    {
        Vector3 position = new Vector3();
        
        position.x = coord.x * unitGridSize;
        position.z = coord.y * unitGridSize;
        
        return position;
    }
    
    private void CreateGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2Int coord = new Vector2Int(i, j);
                Node node = new Node(coord, true);
                grid.Add(coord, node);
                
                //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //Vector3 position = new Vector3(coord.x * unitGridSize, 0, coord.y * unitGridSize);
                //sphere.transform.position = position;
                //sphere.transform.SetParent(transform);
            }
        }
    }
}
