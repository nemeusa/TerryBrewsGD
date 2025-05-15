using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NPCRequest : MonoBehaviour
{
    public string requestedItem;

    public string currentRequest;

    private string selectedDrink = null;
    private int score = 0;

    public bool ocuppy;

    public bool order;
    Client client;

    private void Start()
    {
        client = GetComponent<Client>();
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    OrderText orderText = other.GetComponent<OrderText>();
    //    if (orderText != null && !orderText.ocuppy)
    //    {
    //        orderText.textoUI.text = currentRequest;
    //        orderText.ocuppy = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    OrderText orderText = other.GetComponent<OrderText>();
    //    if (orderText != null && orderText.ocuppy)
    //    {
    //        orderText.ocuppy = false;
    //    }
    //}

}