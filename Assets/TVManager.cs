using UnityEngine;

public class ActivarTVManagerConQ : MonoBehaviour
{
    [Header("Objetos a controlar")]
    [SerializeField] private GameObject TVManager;
    [SerializeField] private GameObject canvasActivoEnEscena;

    public bool yaActivado = false;

    private void Update()
    {
        if (yaActivado)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivarYDesactivarCanvas();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    ActivarYDesactivarCanvas();
                }
            }
        }
    }

    private void ActivarYDesactivarCanvas()
    {
        if (TVManager != null)
            TVManager.SetActive(true);

        if (canvasActivoEnEscena != null)
            canvasActivoEnEscena.SetActive(false);

        yaActivado = true;
        this.enabled = false;
    }
}
