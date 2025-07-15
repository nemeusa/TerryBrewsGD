using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chair : MonoBehaviour
{
    public bool isOcupped{ get; private set; }
    [HideInInspector]
    public Transform seatPosition;
    public GameObject globo;
    public TMP_Text textoCharla;
    public TMP_Text textoPedido;
    public TMP_Text textoNames;
    public TMP_Text textoProfesion;

    private void Start()
    {
        seatPosition = this.transform;
    }

    public void Ocuppy()
    {
        if (!isOcupped)
        {
            isOcupped = true;
           // Debug.Log("Ocupado");
            GetComponent<Renderer>().material.color = Color.red;
        }
    } 
    public void Free()
    {
            isOcupped = false;
           // Debug.Log("Libre");
            GetComponent<Renderer>().material.color = Color.green;
    }

}
