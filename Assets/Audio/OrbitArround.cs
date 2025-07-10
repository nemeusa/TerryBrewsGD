using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    [Tooltip("El objeto alrededor del cual orbitar")]
    public Transform target;

    [Tooltip("Velocidad de rotación (grados por segundo)")]
    public float rotationSpeed = 90f;

    void Update()
    {
        if (target != null)
        {
            // Orbitar alrededor del objeto en el eje Y
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
