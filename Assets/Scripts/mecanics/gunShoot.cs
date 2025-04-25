using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public LookInteraction lookInteraction; // Arrastrás el Player al Inspector
    public GameObject muzzleFlashPrefab; // Asignar en Inspector
    public float flashDuration = 0.05f;
    public LayerMask enemyLayer; // Asigna la capa de enemigos en el Inspector
    public float shootRange = 100f; // Ajusta la distancia de disparo según sea necesario

    void Start()
    {
    }

    void Update()
    {
        // Agregar un log para ver si estamos en el Update y si tenemos el arma
        Debug.Log("En Update - tiene arma: " + (lookInteraction.heldWeapon != null)
                  + ", Prefab asignado: " + (muzzleFlashPrefab != null));

        // Verificar si tienes un arma y el prefab de fuego está asignado
        if (lookInteraction.heldWeapon != null && muzzleFlashPrefab != null)
        {
            if (Input.GetMouseButtonDown(0)) // Si haces clic izquierdo
            {
                Debug.Log("Llamando a Shoot...");
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Disparo");

        // Buscar el punto donde saldrá el disparo (debe estar dentro del arma como hijo)
        Transform muzzle = lookInteraction.heldWeapon.transform.Find("MuzzleFlashPoint");
        if (muzzle == null)
        {
            Debug.LogWarning("No se encontró el MuzzleFlashPoint.");
            return;
        }

        // Instanciar el efecto visual del disparo en ese punto
        GameObject flash = Instantiate(muzzleFlashPrefab, muzzle.position, muzzle.rotation, muzzle);
        Destroy(flash, flashDuration); // Destruir después de un tiempo para que no se acumule

        // Disparar un rayo para detectar el enemigo
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, enemyLayer))
        {
            Debug.Log("¡Enemigo alcanzado!");
            // Eliminar al enemigo
            Destroy(hit.collider.gameObject); // Destruir el GameObject del enemigo
        }
    }
}