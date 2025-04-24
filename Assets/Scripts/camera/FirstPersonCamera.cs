using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {


        Cursor.lockState = CursorLockMode.Locked; // Oculta y bloquea el cursor al centro


        // Usamos la rotación actual de la cámara como punto de partida
        Vector3 angles = transform.eulerAngles;
        xRotation = angles.x;
        yRotation = angles.y;

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // para evitar girar de más hacia arriba/abajo

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
