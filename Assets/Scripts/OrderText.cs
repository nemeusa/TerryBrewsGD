using TMPro;
using UnityEngine;

public class OrderText : MonoBehaviour
{
    public TMP_Text textoUI;
    public bool ocuppy;


    private void OnTriggerEnter(Collider other)
    {
        if (!ocuppy)
        {
            Client client = other.GetComponent<Client>();


            if (client != null)
            {
                textoUI.text = client.currentRequest;
                ocuppy = true;
                client.ocuppy = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ocuppy)
        {
            Client client = other.GetComponent<Client>();

            if (client != null)
            {
                ocuppy = false;
                client.ocuppy = false;
            }
        }
    }
}
