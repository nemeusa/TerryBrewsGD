using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask _beverageLayer;
    [SerializeField] LayerMask _clientLayer;

    public string currentRequest;

    private string _selectedDrink = null;

    private int _score = 0;

    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _selectionText;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _beverageLayer))
            {
                Beverage drinkType = hit.collider.GetComponent<Beverage>();
                if (drinkType != null)
                {
                    _selectedDrink = drinkType.drinkType.ToString();
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, _clientLayer))
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

        if (_score < 0) _score = 0;
        _scoreText.text = "Puntos: " + _score;
        _selectionText.text = "Tienes: " + _selectedDrink;
    }

    public void EntregarBebida(Client client)
    {

        if (_selectedDrink == null) return;

        if (_selectedDrink == currentRequest)
        {
            Debug.Log("pedido correcto");
            _score += 100;
            client.goodOrder = true;
        }
        else
            _score -= 50;


        _selectedDrink = null;
    }

}
