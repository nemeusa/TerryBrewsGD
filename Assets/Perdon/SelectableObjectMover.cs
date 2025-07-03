using UnityEngine;

public class SelectableObjectMover : MonoBehaviour
{
    [Header("Layer de objetos seleccionables")]
    [SerializeField] private LayerMask selectableLayer;

    [Header("Offset del movimiento (altura del plano)")]
    [SerializeField] private float yPlane = 0f;

    private Transform selectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TrySelectObject();
        }

        if (selectedObject != null && Input.GetMouseButton(0))
        {
            MoveSelectedObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectedObject = null;
        }
    }

    void TrySelectObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, selectableLayer))
        {
            selectedObject = hit.transform;
            Debug.Log("Objeto seleccionado: " + selectedObject.name);
        }
    }

    void MoveSelectedObject()
    {
        Plane movePlane = new Plane(Vector3.up, new Vector3(0, yPlane, 0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (movePlane.Raycast(ray, out float distance))
        {
            Vector3 targetPosition = ray.GetPoint(distance);
            selectedObject.position = targetPosition;
        }
    }
}
