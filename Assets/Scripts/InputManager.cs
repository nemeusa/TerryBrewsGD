using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask interactableLayer;
    //public LayerMask pumpLayer;

    public GameObject muerto;

    [SerializeField] Animator animatorClient;

    public bool piumba;

    private Cup vasoSeleccionado;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out Cup vaso))
                {
                    vasoSeleccionado = vaso;
                    //Debug.Log("Vaso Seleccionado: " + vaso.cupSize);
                }
                else if (hit.collider.TryGetComponent(out Beverage botella))
                {
                    if (vasoSeleccionado != null)
                    {
                        vasoSeleccionado.Fill(botella.drinkType);
                    }
                    else
                    {
                        //Debug.Log("Seleccioná un vaso");
                    }
                }
                else if (hit.collider.TryGetComponent(out Pump _pump))
                {
                    StartCoroutine(Client());
                }
            }

            //else if (Physics.Raycast(ray, 100f, interactableLayer))
            //{
            //    Debug.Log("matado");
            //}
        }
    }

    IEnumerator Client()
    {
        //Debug.Log("matado");
        piumba = true;
        //animatorClient.SetBool("Death", true);
        muerto.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //animatorClient.SetBool("Death", false);
        muerto.SetActive(false);
        piumba = false;

    }
}