using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool blocked;
    public Vector2Int cords;
    
    GridManager gridManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCords();
        if (blocked)
        {
            gridManager.BlockNode(cords);
        }
    }

    private void SetCords()
    {
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;
        
        cords = new Vector2Int(x / gridManager.unityGridSize, z / gridManager.unityGridSize);
    }
}
