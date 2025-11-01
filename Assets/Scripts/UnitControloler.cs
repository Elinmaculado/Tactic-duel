using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float movementSpeed = 1f;

    Transform selectedUnit;
    bool unitSelected;
    GridManager gridManager;
    
    List<Node> path = new List<Node>();
    PathFinding pathFinder;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = GetComponent<PathFinding>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (hit.transform.CompareTag("Tile") && unitSelected)
                {
                    //Vector2Int targetCords = hit.transform.GetComponent<Labeler>().cords;
                    //selectedUnit.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);
                    Vector2Int targetCords = hit.transform.GetComponent<Tile>().cords;
                    // Vector2Int startCords = new Vector2Int((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.z) / gridManager.unityGridSize;
                    Vector2Int startCords = gridManager.GetCoordinatesFromPosition(selectedUnit.transform.position);
                    pathFinder.SetNewDestination(startCords, targetCords);
                    RecalculatePath(true);
                }

                if (hit.transform.CompareTag("Unit"))
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }
    }
    
    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathFinder.StartCords;
        }
        else
        {
            coordinates *= gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        // Debug visual del path
        foreach (Node node in path)
        {
            Vector3 pos = gridManager.GetPositionFromCoordinates(node.cords);
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = pos + Vector3.up * 0.5f;
            sphere.transform.localScale = Vector3.one * 0.3f;
            Destroy(sphere, 2f);
        }
        StartCoroutine(FollowPath());
    }
    
    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = selectedUnit.transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords);
            float travelPercent = 0f;

            selectedUnit.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                selectedUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }
    }
}