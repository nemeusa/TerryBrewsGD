using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastJump : MonoBehaviour
{
    public float jumpHeight = 2f;   // Altura del salto
    public float gravity = -9.8f;   // Fuerza de gravedad simulada
    private float initialY;         // Posición inicial del objeto (suelo)
    private bool isJumping = false; // Para controlar el estado del salto
    private float velocityY = 0f;   // Velocidad vertical

    public AudioClip jumpSound;     // El clip de sonido para el salto
    private AudioSource audioSource; // Componente AudioSource


    void Start()
    {
        // Guardar la posición inicial en Y (suelo)
        initialY = transform.position.y;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detectar el salto solo si no está saltando
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping)
        {
            Jump();
        }

        // Aplicar gravedad
        if (isJumping)
        {
            velocityY += gravity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y + velocityY * Time.deltaTime, transform.position.z);
        }

        // Detectar si tocamos el suelo
        if (transform.position.y <= initialY)
        {
            transform.position = new Vector3(transform.position.x, initialY, transform.position.z);  // Asegurarse de que no pase del suelo
            isJumping = false;
            velocityY = 0f;  // Resetear la velocidad vertical
        }
    }

    void Jump()
    {
        isJumping = true;
        velocityY = jumpHeight; // Velocidad inicial del salto

        if (audioSource != null && jumpSound != null)
        {
            Debug.Log("**salta**");
            audioSource.PlayOneShot(jumpSound);
        }
    }
}
