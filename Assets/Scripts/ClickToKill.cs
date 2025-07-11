using UnityEngine;

public class ClickToKill : MonoBehaviour
{
    private Camera _camera;
    private Dissolve _dissolve;

    private void Start()
    {
        _camera = Camera.main;
        _dissolve = GetComponent<Dissolve>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Kill();
                }
            }
        }
    }

    private void Kill()
    {
        if (_dissolve != null)
        {
            _dissolve.TriggerDissolve();
            enabled = false; // Evita múltiples clics
        }
    }
}
