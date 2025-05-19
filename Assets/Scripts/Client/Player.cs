using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask beverageLayer;
    public LayerMask clientLayer;

    public string currentRequest;

    private string selectedDrink = null;

    private int score = 0;

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
                Client client = hit.collider.GetComponent<Client>();

                if (client != null)
                { 
                    currentRequest = client.currentRequest.ToString();
                    Debug.Log("pide : " + currentRequest);
                }

                EntregarBebida(client);
            }
        }
    }

    public void EntregarBebida(Client client)
    {
        if (score < 0) score = 0;

        if (selectedDrink == null) return;

        if (selectedDrink == currentRequest)
        {
            Debug.Log("pedido correcto");
            client.goodOrder = true;
        }
    }

}
