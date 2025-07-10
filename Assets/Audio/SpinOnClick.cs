using UnityEngine;

public class SpinOnClick : MonoBehaviour
{
    private bool isRotating = false;

    void OnMouseDown()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateForSeconds(1f));
        }
    }

    System.Collections.IEnumerator RotateForSeconds(float duration)
    {
        isRotating = true;
        float elapsed = 0f;
        float rotationSpeed = 720f; // grados por segundo

        while (elapsed < duration)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isRotating = false;
    }
}
