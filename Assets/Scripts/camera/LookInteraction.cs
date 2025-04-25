using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LookInteraction : MonoBehaviour
{
    private Vector3 weaponOriginalPosition;
    private Quaternion weaponOriginalRotation;
    private Transform weaponOriginalParent;
    public GameObject CrossHair;
    public GameObject CrossHairReticula;
    public float interactionDistance = 3f;
    public LayerMask interactuable;
    public TextMeshProUGUI useText;
    public string targetObjectName = "shotgun";
    public Transform weaponHolder;

    public GameObject heldWeapon;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // botón derecho
        {
            DropWeapon();
        }

        if (heldWeapon != null)
        {
            useText.gameObject.SetActive(false); // ya agarraste algo
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactuable))
        {
            GameObject target = hit.collider.gameObject;

            if (target.name == targetObjectName)
            {
                useText.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0)) // Click izquierdo
                {
                    PickUpWeapon(target);
                }
            }
            else
            {
                useText.gameObject.SetActive(false);
            }
        }
        else
        {
            useText.gameObject.SetActive(false);
        }
    }

    void PickUpWeapon(GameObject weapon)
    {
        weaponOriginalPosition = weapon.transform.position;
        weaponOriginalRotation = weapon.transform.rotation;
        weaponOriginalParent = weapon.transform.parent;

        CrossHair.SetActive(false);
        CrossHairReticula.SetActive(true);

        Collider col = weapon.GetComponent<Collider>();
        if (col) col.enabled = false;

        weapon.transform.SetParent(weaponHolder);

        // Fijar posición
        weapon.transform.localPosition = Vector3.zero;

        // Fijar rotación específica (ajustá según tu modelo)
       weapon.transform.localRotation = Quaternion.Euler(0f, 90f, 0f); // ejemplo si el arma apunta al costado

        heldWeapon = weapon;
    }

    void DropWeapon()
    {
        if (heldWeapon != null)
        {
            // Quitar el arma de la mano
            heldWeapon.transform.SetParent(weaponOriginalParent);
            heldWeapon.transform.position = weaponOriginalPosition;
            heldWeapon.transform.rotation = weaponOriginalRotation;

            // Reactivar su collider para que vuelva a ser interactuable
            Collider col = heldWeapon.GetComponent<Collider>();
            if (col) col.enabled = true;

            // Limpiar referencia
            heldWeapon = null;

            // Restaurar cursor
            CrossHair.SetActive(true);
            CrossHairReticula.SetActive(false);
        }
    }
}
