using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NPCRequest : MonoBehaviour
{
    public static NPCRequest instance;

    public LayerMask beverageLayer;
    public LayerMask clientLayer;

    public string requestedItem;

    public string currentRequest;

    private string selectedDrink = null;
    private int score = 0;

    public bool ocuppy;

    public bool goodOrder;
    Client client;

    private void Start()
    {
        client = GetComponent<Client>();
        NuevaPeticion();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, beverageLayer))
            {
                Beverage drinkType = hit.collider.GetComponent<Beverage>();
                if (drinkType != null)
                {
                    selectedDrink = drinkType.drinkType.ToString();
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, clientLayer))
            {

                EntregarBebida();
            }
        }
    }

    public void NuevaPeticion()
    {
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        currentRequest = opciones[Random.Range(0, opciones.Length)];
    }


    public void EntregarBebida()
    {
        if (score < 0) score = 0;

        if (selectedDrink == null) return;

        if (selectedDrink == currentRequest)
        {
            Debug.Log("pedido correcto");
            goodOrder = true;
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