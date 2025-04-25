using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShoot : MonoBehaviour
{
    public LookInteraction lookInteraction; // Arrastrás el Player al Inspector
    public GameObject muzzleFlashPrefab; // asignar en Inspector
    public float flashDuration = 0.05f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Agregar sun log para ver si estamos en el Update y si tenemos el arma
        Debug.Log("En Update - tiene arma: " + (lookInteraction.heldWeapon != null) + ", Prefab asignado: " + (muzzleFlashPrefab != null));

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
    }
}