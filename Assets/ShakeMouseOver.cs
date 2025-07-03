using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeMouseOver : MonoBehaviour
{
    [SerializeField] private MoveDrinks moveDrinks;

    [Header("Configuración del sacudido")]
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.05f;
    [SerializeField] private float shakeSpeed = 0.025f;

    private bool isShaking = false;
    private Vector3 originalPosition;
    private float shakeTimer;

    private void Start()
    {
        originalPosition = transform.localPosition;
        originalPosition = transform.localPosition;
        moveDrinks = GetComponent<MoveDrinks>();
    }

    private void Update()
    {
        if (moveDrinks != null && moveDrinks.isDraggingDrink)
        {
            return;
        }

        DetectMouseHover();

        if (isShaking)
        {
            shakeTimer -= Time.deltaTime;

            if (shakeTimer > 0f)
            {
                float offsetX = Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
                float offsetY = Mathf.Cos(Time.time * shakeSpeed) * shakeMagnitude;
                transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
            }
            else
            {
                StopShaking();
            }
        }
    }

    private void DetectMouseHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                // Si se está haciendo clic sobre ESTE objeto, detener el sacudido
                if (Input.GetMouseButton(0))
                {
                    StopShaking();
                    return;
                }

                if (!isShaking)
                {
                    StartShaking();
                }
            }
        }
    }

    private void StartShaking()
    {
        isShaking = true;
        shakeTimer = shakeDuration;
        originalPosition = transform.localPosition;
    }

    private void StopShaking()
    {
        isShaking = false;
        transform.localPosition = originalPosition;
    }
}
