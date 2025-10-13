using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class Labeler : MonoBehaviour
{
    private TextMeshPro label;
    public Vector2Int cords = new Vector2Int();
    GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponentInChildren<TextMeshPro>();
        
        DisplayCords();
    }

    private void Update()
    {
        DisplayCords();
        transform.name = cords.ToString();
    }

    private void DisplayCords()
    {
        if(!gridManager)
            return;
        
        cords.x = Mathf.RoundToInt(transform.position.x / gridManager.unityGridSize);
        cords.y = Mathf.RoundToInt(transform.position.z / gridManager.unityGridSize);

        label.text = $"{cords.x},{cords.y}";
    }
}
