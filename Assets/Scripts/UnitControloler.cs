using UnityEngine;
using UnityEngine.InputSystem; // Importante

public class UnitController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float movementSpeed = 1f;

    Transform selectedUnit;
    bool unitSelected;
    GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // Nuevo Input System: usa Mouse.current
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Tile") && unitSelected)
                {
                    Vector2Int targetCords = hit.transform.GetComponent<Labeler>().cords;
                    selectedUnit.position = new Vector3(targetCords.x, selectedUnit.position.y, targetCords.y);
                }

                if (hit.transform.CompareTag("Unit"))
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                }
            }
        }
    }
}