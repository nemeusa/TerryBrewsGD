using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCamera : MonoBehaviour
{
    public float Velocity = 100f;
    float RotacionX = 0f;
    public Transform Player;

    void Start()
    {

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Velocity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Velocity * Time.deltaTime;

        RotacionX -= mouseY;

        RotacionX = Mathf.Clamp(RotacionX, 0f, 1f);

        transform.localRotation = Quaternion.Euler(RotacionX, 0f, 0f);

        Player.Rotate(Vector3.up * mouseX);

    }
}
