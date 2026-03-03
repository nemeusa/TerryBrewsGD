using UnityEngine;

public class HoverRaycast : MonoBehaviour
{
    public LayerMask interactableLayer;
    private ShaderOutline current;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
        {
            ShaderOutline target = hit.collider.GetComponent<ShaderOutline>();

            if (target != current)
            {
                Clear();
                current = target;
                if (current != null)
                    current.EnableOutline(true);
            }
        }
        else
        {
            Clear();
        }
    }
    void Clear()
    {
        if (current != null)
            current.EnableOutline(false);
        current = null;
    }
}
