using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NPCRequest : MonoBehaviour
{
    public string requestedItem;

    private string currentRequest;

    private string selectedDrink = null;
    private int score = 0;

    public bool order;

    private void Start()
    {
        NuevaPeticion();
    }
    public void NuevaPeticion()
    {
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        currentRequest = opciones[Random.Range(0, opciones.Length)];
    }


    void EntregarBebida()
    {
        if (score < 0) score = 0;

        if (selectedDrink == null) return;

        if (selectedDrink == currentRequest)
        {
            order = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OrderText orderText = other.GetComponent<OrderText>();
        if (orderText != null)
        {
            orderText.textoUI.text = currentRequest;
        }
    }

}